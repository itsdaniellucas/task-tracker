<template>
    <v-sheet min-height="85vh"
                max-height="85vh"
                rounded="lg"
                style="overflow: hidden;">
        <v-row justify="space-between">
            <v-col xl="3" lg="3" md="3">
                <h2 class="ml-3">{{ classification.name }}</h2>
            </v-col>
            <v-col xl="2" lg="3" md="3">
                <TaskDialog :mode="'add'" :classification="classification" :disabled="!state.user.isLoggedIn" @on-save="onTaskSave" />
            </v-col>
        </v-row>
        <v-divider />
        <v-sheet min-height="75vh" 
                    max-height="75vh" 
                    style="overflow: auto;">
            <TaskItem v-for="t in tasks" 
                        :key="t.id" 
                        :task="t"
                        :move-left-disabled="isPrevClassEmpty"
                        :move-right-disabled="isNextClassEmpty"
                        :disabled="!state.user.isLoggedIn"
                        @on-status-change="onTaskStatusChange"
                        @on-move-left="onTaskClassificationChange($event, prevClassification)"
                        @on-move-right="onTaskClassificationChange($event, nextClassification)" />
        </v-sheet>
    </v-sheet>
</template>

<script>
    import TaskItem from '@/components/TaskItem'
    import TaskDialog from '@/views/dialogs/TaskDialog'
    import state from '@/state'
    import utility from '@/utility'

    export default {
        name: 'TaskColumn',
        
        components: {
            TaskItem,
            TaskDialog
        },

        props: {
            tasks: {
                type: Array,
                default: () => [],
            },
            classification: {
                type: Object,
                default: () => ({}),
            },
            prevClassification: {
                type: Object,
                default: () => ({}),
            },
            nextClassification: {
                type: Object,
                default: () => ({}),
            }
        },

        data: () => ({
            state,
        }),

        computed: {
            isPrevClassEmpty() {
                return utility.isEmptyObject(this.prevClassification);
            },
            isNextClassEmpty() {
                return utility.isEmptyObject(this.nextClassification);
            }
        },

        methods: {
            onTaskSave($event) {
                state.tasks.addTask($event.task);
            },
            onTaskStatusChange($event) {
                state.tasks.changeTaskStatus($event.task, $event.status);
            },
            onTaskClassificationChange($event, classification) {
                state.tasks.changeTaskClassification($event.task, classification);
            },
        }
    }
</script>

<style scoped>

</style>