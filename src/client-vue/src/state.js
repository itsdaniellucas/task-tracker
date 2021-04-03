import Vue from 'vue'
import utility from '@/utility'
import ResourceService from '@/services/resourceService'
import TaskService from '@/services/taskService'
import SprintService from '@/services/sprintService'
import SignalRService from '@/services/signalRService'

const state = new Vue({
    data: (parent) => ({
        tasks: tasks(parent),
        classifications: classifications(),
        sprints: sprints(),
        user: user(),
        alert: alert(),
        isFetching: false,
    }),
})

function user() {
    return new Vue({
        data: () => ({
            value: null,
        }),

        computed: {
            isLoggedIn() {
                return this.value != null;
            },
            isAdmin() {
                return this.isLoggedIn && this.value.Role.Name == 'Admin';
            }
        },

        methods: {
            setUser(user) {
                this.value = user ? { ...user } : null;
            }
        }
    });
}

function sprints() {
    return new Vue({
        data: () => ({
            values: [],
            current: 0,
        }),

        methods: {
            fetch() {
                return ResourceService.getSprints().then(data => {
                    this.values = data.map(i => this.fromServerModel(i));
                })
            },
            setCurrentSprint(sprintId) {
                this.current = sprintId;
            },
            addSprint(sprintName) {
                return SprintService.addSprint({ Name: sprintName });
            },
            fromServerModel(s) {
                return {
                    id: s.Id,
                    name: s.Name,
                }
            },
        }
    })
}

function alert() {
    return new Vue({
        data: () => ({
            visible: false,
            isProgress: true,
            icon: '',
            text: 'Saving changes made...',
            isWhiteText: true,
            color: 'info',
            timeout: 5000,
            timeoutFn: null,
            type: 'saving',
        }),

        methods: {
            saving(text = '', autoFade = false) {
                if(this.timeoutFn) {
                    clearTimeout(this.timeoutFn);
                }

                this.type = 'saving';
                this.visible = true;
                this.isProgress = true;
                this.icon = '';
                this.text = text || 'Saving changes made...';
                this.isWhiteText = true;
                this.color = 'info';

                if(autoFade) {
                    this.timeoutFn = setTimeout(() => {
                        this.visible = false;
                    }, this.timeout);
                }
            },
            
            success(text = '', autoFade = true) {
                if(this.timeoutFn) {
                    clearTimeout(this.timeoutFn);
                }

                this.type = 'success';
                this.visible = true;
                this.isProgress = false;
                this.icon = 'mdi-check-circle-outline';
                this.text = text || 'Changes have been saved!';
                this.isWhiteText = true;
                this.color ='success';

                if(autoFade) {
                    this.timeoutFn = setTimeout(() => {
                        this.visible = false;
                    }, this.timeout);
                }
            },

            error(text = '', autoFade = true) {
                if(this.timeoutFn) {
                    clearTimeout(this.timeoutFn);
                }

                this.type = 'error';
                this.visible = true;
                this.isProgress = false;
                this.icon = 'mdi-alert-circle-outline';
                this.text = text || 'An error occured while saving!';
                this.isWhiteText = true;
                this.color ='error';

                if(autoFade) {
                    this.timeoutFn = setTimeout(() => {
                        this.visible = false;
                    }, this.timeout);
                }
            }
        }
    });
}


function classifications() {
    return new Vue({
        data: () => ({
            values: [],
        }),

        computed: {
            map() {
                return utility.toObjectMap(this.values, 'name');
            },
            backlog() {
                return this.map['Backlog'];
            },
            active() {
                return this.map['Active'];
            },
            closed() {
                return this.map['Closed'];
            },
            archived() {
                return this.map['Archived'];
            }
        },

        methods: {
            fetch() {
                return ResourceService.getClassifications().then(data => {
                    this.values = data.map(i => this.fromServerModel(i));
                })
            },
            fromServerModel(s) {
                return {
                    id: s.Id,
                    name: s.Name,
                }
            },
        }
    })
}

function tasks(parent) {
    return new Vue({
        data: () => ({
            values: []
        }),
        
        methods: {
            changeTaskStatus(task, status) {
                let origTask = this.values.find(i => i.id == task.id);
                if(origTask) {
                    origTask.isCompleted = status;
                    this.modifyTask(origTask);
                }
            },
            changeTaskClassification(task, classification) {
                let origTask = this.values.find(i => i.id == task.id);
                if(origTask) {
                    origTask.classificationId = classification.id;
                    this.modifyTask(origTask);
                }
            },
            fetch(sprintId) {
                return ResourceService.getTasks({ sprintId: sprintId }).then(data => {
                    this.values = data.map(i => this.fromServerModel(i));
                })
            },
            addTask(task) {
                return TaskService.addTask(this.toServerModel(task)).then(() => {
                    this.values.push(task);
                    SignalRService.notifyTaskChanges(parent.sprints.current);
                });
            },
            modifyTask(task) {
                return TaskService.modifyTask(this.toServerModel(task)).then(() => {
                    SignalRService.notifyTaskChanges(parent.sprints.current);
                });
            },
            removeTask(task) {
                return TaskService.removeTask(this.toServerModel(task)).then(() => {
                    SignalRService.notifyTaskChanges(parent.sprints.current);
                });
            },
            toServerModel(t) {
                return {
                    Id: t.id,
                    Title: t.name,
                    Description: t.description,
                    ExpectedHours: parseInt(t.expectedTime),
                    ActualHours: parseInt(t.actualTime),
                    IsCompleted: t.isCompleted,
                    ClassificationId: t.classificationId,
                    SprintId: t.sprintId,
                }
            },
            fromServerModel(t) {
                return {
                    id: t.Id,
                    name: t.Title,
                    description: t.Description,
                    expectedTime: t.ExpectedHours,
                    actualTime: t.ActualHours,
                    isCompleted: t.IsCompleted,
                    classificationId: t.ClassificationId,
                    sprintId: t.SprintId,
                }
            },
        }
    });
}

export default state