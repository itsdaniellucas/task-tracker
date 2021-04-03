import React, { useState, useEffect } from 'react';
import { Grid } from '@material-ui/core';
import Chart from '../../components/Chart';
import c3 from 'c3';
import state from '../../state'


export default function Statistics(props) {
    const [data, setData] = useState([]);

    useEffect(() => {
        const sub = state.tasks.values.subscribe(x => {
            setData(x)
        });

        return () => sub.unsubscribe();
    }, []);

    const taskCompletionChart = (ref, data) => {
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
    }

    const timelinessChart = (ref, data) => {
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
    }

    const typeCompletionChart = (ref, data) => {
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

    return (
        <Grid container spacing={4}>
            <Grid item xs={4}><Chart title="Task Completion" implementation={taskCompletionChart} data={data} /></Grid>
            <Grid item xs={4}><Chart title="Timeliness" implementation={timelinessChart} data={data} /></Grid>
            <Grid item xs={4}><Chart title="Completion By Type" implementation={typeCompletionChart} data={data} /></Grid>
        </Grid>
    )
}