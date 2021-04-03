<template>
    <v-row>
        <v-col cols="4">
            <Chart :title="'Task Completion'" :implementation="taskCompletionChart" :chart-data="state.tasks.values" />
        </v-col>
        <v-col cols="4">
            <Chart :title="'Timeliness'" :implementation="timelinessChart" :chart-data="state.tasks.values" />
        </v-col>
        <v-col cols="4">
            <Chart :title="'Completion By Type'" :implementation="typeCompletionChart" :chart-data="state.tasks.values" />
        </v-col>
    </v-row>
</template>

<script>
    import Chart from '@/components/Chart'
    import state from '@/state'
    import c3 from 'c3'

    export default {
        name: 'Statistics',

        components: {
            Chart,
        },

        data: () => ({
            state,
        }),

        methods: {
            taskCompletionChart(ref, data) {
                let completedCount = data.filter(i => i.isCompleted).length;
                let notCompletedCount = data.length - completedCount;

                let chartData = [
                    ['Completed', completedCount],
                    ['Not Completed', notCompletedCount]
                ];

                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'donut',
                    },
                    donut: {
                        title: 'Task Completion'
                    }
                });
            },
            timelinessChart(ref, data) {
                let onTimeCount = data.filter(i => i.actualTime <= i.expectedTime).length;
                let notOnTimeCount = data.length - onTimeCount;

                let chartData = [
                    ['On Time', onTimeCount],
                    ['Not On Time', notOnTimeCount]
                ];

                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'pie',
                    },
                });
            },
            typeCompletionChart(ref, data) {
                let completed = ['Completed', 0, 0, 0, 0];
                let notCompleted = ['Not Completed', 0, 0, 0, 0];

                data.forEach(i => {
                    if(i.isCompleted) {
                        completed[i.classificationId] += 1;
                    } else {
                        notCompleted[i.classificationId] += 1;
                    }
                })

                let chartData = [
                    completed,
                    notCompleted,
                ];

                return c3.generate({
                    bindto: ref,
                    data: {
                        columns: chartData,
                        type: 'bar',
                        groups: [
                            ['Completed', 'Not Completed']
                        ],
                    },
                    axis: {
                        x: {
                            type: "category",
                            categories: ['Backlog', 'Active', 'Closed', 'Archived']
                        }
                    }
                });
            }
        }
    }
</script>

<style scoped>

</style>