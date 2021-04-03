import React, { useState, useCallback } from 'react';
import { makeStyles, withStyles } from '@material-ui/core/styles';
import { yellow } from '@material-ui/core/colors';
import { Grid, Card, CardActions, CardContent, Typography, Switch, IconButton } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import TimerIcon from '@material-ui/icons/Timer';
import TaskDialog from '../views/dialogs/TaskDialog'
import actions from '../actions'


const YellowSwitch = withStyles({
    switchBase: {
        color: yellow[50],
        '&$checked': {
          color: yellow[500],
        },
        '&$checked + $track': {
            backgroundColor: yellow[500],
        },
    },
    checked: {},
    track: {},
})(Switch);

export default function TaskItem(props) {

    const [task, setTask] = useState({ ...props.task });

    const useStyles = makeStyles((theme) => ({
        root: {
            margin: theme.spacing(2),
            backgroundColor: task.isCompleted ? '#43a047' : '#fbc02d',
            color: task.isCompleted ? '#ffffff' : '#424242',
        },
        title: {
            fontSize: 16,
            color: task.isCompleted ? '#ffffff' : '#424242',
            textDecoration: task.isCompleted ? 'line-through' : 'none',
        },
        body: {
            color: task.isCompleted ? '#ffffff' : '#424242',
            textDecoration: task.isCompleted ? 'line-through' : 'none',
        },
    }));

    const classes = useStyles();

    const onStatusChange = (event) => {
        if(props.onStatusChange) {
            setTask({...task, isCompleted: event.target.checked})
            props.onStatusChange({
                task: task,
                status: event.target.checked
            })
        }
    }

    const onMoveLeft = () => {
        if(props.onMoveLeft) {
            props.onMoveLeft(task);
        }
    }

    const onMoveRight = () => {
        if(props.onMoveRight) {
            props.onMoveRight(task);
        }
    }

    const onTaskSave = useCallback((event) => {
        setTask({...event.task})
        actions.tasks.modifyTask(event.task);
    }, [])

    const onTaskRemove = useCallback(() => {
        actions.tasks.removeTask(task);
    }, [task])
 
    return (
        <Card className={classes.root}>
            <CardContent>
                <Grid container alignItems="center">
                    <Grid item xl={11} lg={11} md={10}>
                        <Grid container alignItems="center">
                            <Grid item>
                                <YellowSwitch color="primary" checked={task.isCompleted} onChange={onStatusChange} disabled={props.disabled} />
                            </Grid>
                            <Grid>
                                <Typography className={classes.title} color="textPrimary">
                                    {task.name}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Grid>
                    <Grid item xl={1} lg={1} md={2}>
                        <IconButton size="small" onClick={onTaskRemove} disabled={props.disabled} className={classes.body}>
                            <CloseIcon />
                        </IconButton>
                    </Grid>
                </Grid>
                
                <Typography color="textSecondary" className={classes.body}>
                    {task.description}
                </Typography>
            </CardContent>
            <CardActions>
                <Grid container alignItems="center" justify="space-between">
                    <Grid item xl={4} lg={4} md={5}>
                        <Grid container alignItems="center">
                            <Grid item>
                                <TimerIcon />
                            </Grid>
                            <Grid item>
                                <Typography color="textPrimary" className={classes.body}>
                                    {task.actualTime} / {task.expectedTime}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Grid>
                    <Grid item xl={3} lg={5} md={7}>
                        <TaskDialog size="small" mode="edit" task={task} onSave={onTaskSave} disabled={props.disabled} className={classes.body} />
                        <IconButton size="small" onClick={onMoveLeft} disabled={props.moveLeftDisabled || props.disabled} className={classes.body}>
                            <ArrowBackIcon />
                        </IconButton>
                        <IconButton size="small" onClick={onMoveRight} disabled={props.moveRightDisabled || props.disabled} className={classes.body}>
                            <ArrowForwardIcon />
                        </IconButton>
                    </Grid>
                </Grid>
            </CardActions>
        </Card>
    );
}