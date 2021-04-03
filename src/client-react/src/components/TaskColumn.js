import React, { useState, useEffect, useCallback } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Paper, Divider, Grid, Typography } from '@material-ui/core';
import TaskItem from './TaskItem'
import TaskDialog from '../views/dialogs/TaskDialog'
import state from '../state'
import actions from '../actions'

const useStyles = makeStyles((theme) => ({
    root: {
        minHeight: '85vh',
        maxHeight: '85vh',
        overflow: 'hidden',
    },
    body: {
        minHeight: '75vh',
        maxHeight: '75vh',
        overflow: 'auto',
    },
    title: {
        fontSize: 32,
        margin: theme.spacing(1),
        marginLeft: theme.spacing(2),
    },
    add: {
        marginRight: theme.spacing(2),
        marginTop: theme.spacing(1),
    }
}));

export default function TaskColumn(props) {
    const classes = useStyles();
    const [isLoggedIn, setIsLoogedIn] = useState(false);
    const title = props.classification ? props.classification.name : '';

    useEffect(() => {
        const sub = state.user.value.subscribe(x => {
            setIsLoogedIn(x != null)
        });

        return () => sub.unsubscribe();
    }, []);

    const onTaskSave = useCallback((event) => {
        actions.tasks.addTask(event.task);
    }, [])

    const onTaskStatusChange = useCallback((event) => {
        actions.tasks.changeTaskStatus(event.task, event.status);
    }, [])

    const onTaskClassificationChange = useCallback((task, classification) => {
        actions.tasks.changeTaskClassification(task, classification);
    }, [])

    return (
        <Paper elevation={4} className={classes.root}>
            <Grid container justify="space-between">
                <Grid item><Typography className={classes.title} color="textPrimary">{title}</Typography></Grid>
                <Grid item><TaskDialog mode="add" className={classes.add} onSave={onTaskSave} disabled={!isLoggedIn} classification={props.classification} /></Grid>
            </Grid>
            <Divider />
            <Paper elevation={0} className={classes.body}>
                {props.tasks.map((x) => 
                    <TaskItem key={JSON.stringify(x)} 
                                task={x}
                                moveLeftDisabled={props.prev == null}
                                moveRightDisabled={props.next == null}
                                disabled={!isLoggedIn}
                                onStatusChange={onTaskStatusChange}
                                onMoveLeft={(t) => onTaskClassificationChange(t, props.prev)}
                                onMoveRight={(t) => onTaskClassificationChange(t, props.next)}  />
                )}
            </Paper>
        </Paper>
    )
}
