import state from './state'
import resourceService from './services/resourceService'
import taskService from './services/taskService'
import sprintService from './services/sprintService'
import signalRService from './services/signalRService'

function toServerModelTask(t) {
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
}

function modifyTask(task) {
    return taskService.modifyTask(toServerModelTask(task)).then(() => {
        signalRService.notifyTaskChanges(state.sprints.current);
    });
}

const taskActions = {
    changeTaskStatus: (task, status) => {
        let arr = [...state.tasks.values.value];
        let origTask = arr.find(i => i.id == task.id);
        if(origTask) {
            origTask.isCompleted = status;
            return modifyTask(origTask);
        }
    },
    changeTaskClassification: (task, classification) => {
        let arr = [...state.tasks.values.value];
        let origTask = arr.find(i => i.id == task.id);
        if(origTask) {
            origTask.classificationId = classification.id;
            return modifyTask(origTask);
        }
    },
    fetch: (sprintId) => {
        return resourceService.getTasks({ sprintId: sprintId }).then(data => {
            const transformedData = data.map(t => ({
                id: t.Id,
                name: t.Title,
                description: t.Description,
                expectedTime: t.ExpectedHours,
                actualTime: t.ActualHours,
                isCompleted: t.IsCompleted,
                classificationId: t.ClassificationId,
                sprintId: t.SprintId,
            }));
            state.tasks.values.next(transformedData);
        })
    },
    addTask: (task) => {
        return taskService.addTask(toServerModelTask(task)).then(() => {
            signalRService.notifyTaskChanges(state.sprints.current);
        });
    },
    modifyTask: modifyTask,
    removeTask: (task) => {
        return taskService.removeTask(toServerModelTask(task)).then(() => {
            signalRService.notifyTaskChanges(state.sprints.current);
        });
    }
}

const sprintActions = {
    fetch: () => {
        return resourceService.getSprints().then(data => {
            const transformedData = data.map(i => ({id: i.Id, name: i.Name}));
            state.sprints.values.next(transformedData);
        })
    },
    setCurrentSprint: (sprintId) => {
        state.sprints.current = sprintId;
    },
    addSprint: (sprintName) => {
        return sprintService.addSprint({ Name: sprintName });
    }
}

const alertActions = {
    saving: (text = '', autoFade = false) => {
        const currentConfig = state.alert.config.value;

        if(currentConfig.timeoutFn) {
            clearTimeout(currentConfig.timeoutFn);
        }

        const newConfig = {
            type: 'saving',
            visible: true,
            isProgress: true,
            text: text || 'Saving changes made...',
            severity: 'info',
            timeout: 5000,
        }

        if(autoFade) {
            newConfig.timeoutFn = setTimeout(() => {
                newConfig.visible = false;
                state.alert.config.next({ ...newConfig });
            }, newConfig.timeout);
        }

        state.alert.config.next(newConfig);
    },
    success: (text = '', autoFade = true) => {
        const currentConfig = state.alert.config.value;

        if(currentConfig.timeoutFn) {
            clearTimeout(currentConfig.timeoutFn);
        }

        const newConfig = {
            type: 'success',
            visible: true,
            isProgress: false,
            text: text || 'Changes have been saved!',
            severity: 'success',
            timeout: 5000,
        }

        if(autoFade) {
            newConfig.timeoutFn = setTimeout(() => {
                newConfig.visible = false;
                state.alert.config.next({ ...newConfig });
            }, newConfig.timeout);
        }

        state.alert.config.next(newConfig);
    },
    error: (text = '', autoFade = true) => {
        const currentConfig = state.alert.config.value;

        if(currentConfig.timeoutFn) {
            clearTimeout(currentConfig.timeoutFn);
        }

        const newConfig = {
            type: 'error',
            visible: true,
            isProgress: false,
            text: text || 'An error occured while saving!',
            severity: 'error',
            timeout: 5000,
        }

        if(autoFade) {
            newConfig.timeoutFn = setTimeout(() => {
                newConfig.visible = false;
                state.alert.config.next({ ...newConfig });
            }, newConfig.timeout);
        }

        state.alert.config.next(newConfig);
    },
}

const classificationActions = {
    fetch: () => {
        return resourceService.getClassifications().then(data => {
            const transformedData = data.map(i => ({id: i.Id, name: i.Name}));
            state.classifications.values.next(transformedData);
        })
    },
}


const actions = {
    tasks: taskActions,
    sprints: sprintActions,
    classifications: classificationActions,
    alert: alertActions,
}

export default actions