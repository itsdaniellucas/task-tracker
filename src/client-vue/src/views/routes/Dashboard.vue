<template>
    <v-row>
        <v-col cols="3">
            <TaskColumn :tasks="backlogTasks" 
                        :classification="state.classifications.backlog" 
                        :next-classification="state.classifications.active" />
        </v-col>
        <v-col cols="3">
            <TaskColumn :tasks="activeTasks" 
                        :classification="state.classifications.active" 
                        :prev-classification="state.classifications.backlog" 
                        :next-classification="state.classifications.closed" />
        </v-col>
        <v-col cols="3">
            <TaskColumn :tasks="closedTasks" 
                        :classification="state.classifications.closed" 
                        :prev-classification="state.classifications.active" 
                        :next-classification="state.classifications.archived" />
        </v-col>
        <v-col cols="3">
            <TaskColumn :tasks="archivedTasks" 
                        :classification="state.classifications.archived" 
                        :prev-classification="state.classifications.closed" />
        </v-col>
    </v-row>
</template>

<script>
    import TaskColumn from '@/components/TaskColumn'
    import state from '@/state'

    export default {
        name: 'Dashboard',

        components: {
            TaskColumn,
        },

        data: () => ({
            state,
        }),

        computed: {
            backlogTasks() {
                return state.tasks.values.filter(i => i.classificationId == state.classifications.backlog.id);
            },
            activeTasks() {
                return state.tasks.values.filter(i => i.classificationId == state.classifications.active.id);
            },
            closedTasks() {
                return state.tasks.values.filter(i => i.classificationId == state.classifications.closed.id);
            },
            archivedTasks() {
                return state.tasks.values.filter(i => i.classificationId == state.classifications.archived.id);
            }
        },
    }
</script>

<style scoped>

</style>