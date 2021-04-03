<template>
    <v-card v-if="task" :color="task.isCompleted ? '#43a047' : '#fbc02d'"
            class="mx-6 my-3"
            :dark="task.isCompleted">
        <v-card-title class="headline pt-2">
            <v-row>
                <v-col xl="10" lg="9" md="9">
                    <v-row>
                        <v-switch class="ma-0 ml-2"
                                color="#fbc02d"
                                hide-details
                                @change="onStatusChange"
                                :input-value="task.isCompleted"
                                :disabled="disabled" />
                        <span :class="{ 'text-decoration-line-through': task.isCompleted }">
                            {{ task.name }}
                        </span>
                    </v-row>
                </v-col>
                <v-col xl="2" lg="3" md="3">
                    <v-btn icon color="black" :disabled="disabled" @click="onTaskRemove">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                </v-col>
            </v-row>
        </v-card-title>

        <v-card-subtitle class="pb-0"   
                        :class="{ 'text-decoration-line-through': task.isCompleted }">
            <span>{{ task.description }}</span>
        </v-card-subtitle>

        <v-card-actions class="py-0">
            <v-row justify="space-between">
                <v-col xl="4" lg="5" md="5">
                    <v-icon size="20">mdi-timer-outline</v-icon> 
                    <span :class="{ 'text-decoration-line-through': task.isCompleted }">{{ task.actualTime }} / {{ task.expectedTime }}</span>
                </v-col>
                <v-col xl="4" lg="7" md="7">
                    <TaskDialog :mode="'edit'" :task="task" :disabled="disabled" @on-save="onTaskSave"  />
                    <v-btn color="secondary"
                            fab
                            x-small
                            class="ml-n1 mr-1"
                            :disabled="moveLeftDisabled || disabled"
                            @click="onMoveLeft">
                        <v-icon>mdi-arrow-left</v-icon>
                    </v-btn>
                    <v-btn color="secondary"
                            fab
                            x-small
                            :disabled="moveRightDisabled || disabled"
                            @click="onMoveRight">
                        <v-icon>mdi-arrow-right</v-icon>
                    </v-btn>
                </v-col>
            </v-row>
        </v-card-actions>
    </v-card>
</template>

<script>
    import TaskDialog from '@/views/dialogs/TaskDialog'
    import state from '@/state'

    export default {
        name: 'TaskItem',

        components: {
            TaskDialog
        },

        props: {
            task: {
                type: Object,
                default: () => ({}),
            },
            disabled: {
                type: Boolean,
                default: false,
            },
            moveLeftDisabled: {
                type: Boolean,
                default: false,
            },
            moveRightDisabled: {
                type: Boolean,
                default: false,
            }
        },

        methods: {
            onTaskSave($event) {
                state.tasks.modifyTask($event.task);
            },
            onTaskRemove() {
                state.tasks.removeTask(this.task);  
            },
            onStatusChange($event) {
                this.$emit('on-status-change', {
                    task: this.task,
                    status: $event,
                });
            },
            onMoveLeft() {
                this.$emit('on-move-left', {
                    task: this.task
                });
            },
            onMoveRight() {
                this.$emit('on-move-right', {
                    task: this.task
                });
            },
        }
    }
</script>

<style scoped>

</style>