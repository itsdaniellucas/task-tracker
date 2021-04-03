import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { FormControlLabel, Divider, Switch, Button, IconButton, Dialog, DialogActions, DialogTitle, DialogContent, Grid } from '@material-ui/core';
import CreateIcon from '@material-ui/icons/Create';
import AddIcon from '@material-ui/icons/Add';
import FingerprintIcon from '@material-ui/icons/Fingerprint';
import DescriptionIcon from '@material-ui/icons/Description';
import HourglassEmptyIcon from '@material-ui/icons/HourglassEmpty';
import HourglassFullIcon from '@material-ui/icons/HourglassFull';
import TextBox from '../../components/TextBox'
import Dropdown from '../../components/Dropdown'
import state from '../../state'

const useStyles = makeStyles((theme) => ({
    content: {
        paddingTop: theme.spacing(2),
        paddingBottom: theme.spacing(2),
    },
    dropdown: {
        marginTop: theme.spacing(2),
        marginLeft: theme.spacing(3.5),
        minWidth: 180,
    },
}));

export default function TaskDialog(props) {
    const classes = useStyles();

    const [name, setName] = useState(props.task ? props.task.name : '');
    const [desc, setDesc] = useState(props.task ? props.task.description : '');
    const [actual, setActual] = useState(props.task ? props.task.actualTime : 0);
    const [expected, setExpected] = useState(props.task ? props.task.expectedTime : 0);
    const [classification, setClassification] = useState(props.task ? props.task.classificationId : (props.classification ?  props.classification.id : 1));
    const [isDone, setIsDone] = useState(props.task ? props.task.isCompleted : false);
    const [visible, setVisible] = useState(false);
    const [classifications, setClassifications] = useState([]);
    const title = props.mode === 'add' ? 'Add Task' : 'Modify Task';

    useEffect(() => {
        const sub = state.classifications.values.subscribe(x => {
            setClassifications(x);
        });

        return () => sub.unsubscribe();
    }, []);

    useEffect(() => {
        setName(props.task ? props.task.name : '');
        setDesc(props.task ? props.task.description : '');
        setActual(props.task ? props.task.actualTime : 0);
        setExpected(props.task ? props.task.expectedTime : 0);
        setClassification(props.task ? props.task.classificationId : (props.classification ?  props.classification.id : 1));
        setIsDone(props.task ? props.task.isCompleted : false);
    }, [props.task, props.classification]);

    const handleOpen = () => {
        setVisible(true);
    }

    const handleClose = () => {
        setVisible(false);
        setName('');
        setDesc('');
        setActual(0);
        setExpected(0);
        setClassification(props.task ? props.task.classificationId : (props.classification ?  props.classification.id : 1));
        setIsDone(false);
    }

    const onValueChange = (value, handler) => {
        handler(value);
    }

    const handleSave = () => {
        if(props.onSave) {
            props.onSave({
                mode: props.mode,
                task: {
                    ...props.task,
                    name: name,
                    description: desc,
                    actualTime: parseInt(actual),
                    expectedTime: parseInt(expected),
                    classificationId: classification,
                    isCompleted: isDone,
                    sprintId: state.sprints.current,
                }
            })
        }

        handleClose();
    }

    return (
        <React.Fragment>
            <IconButton className={props.className} onClick={handleOpen} size={props.size} disabled={props.disabled}>
                { props.mode === 'add' ? <AddIcon /> : <CreateIcon /> }
            </IconButton>
            <Dialog open={visible}
                    onClose={handleClose}
                    size="md"
                    fullWidth>
                <DialogTitle>{title}</DialogTitle>
                <Divider />
                <DialogContent className={classes.content}>
                    <TextBox icon={<FingerprintIcon />} label="Task" width="53" value={name} onTextBoxChange={(v) => onValueChange(v, setName)} />
                    <TextBox icon={<DescriptionIcon />} label="Description" width="53" multiline rows={4} value={desc} onTextBoxChange={(v) => onValueChange(v, setDesc)} />
                    <Grid container spacing={2}>
                        <Grid item xs={6}>
                            <TextBox icon={<HourglassFullIcon />} width="20" label="Actual Time" value={actual} onTextBoxChange={(v) => onValueChange(v, setActual)} />
                        </Grid>
                        <Grid item xs={6}>
                            <TextBox icon={<HourglassEmptyIcon />} width="20" label="Expected Time" value={expected} onTextBoxChange={(v) => onValueChange(v, setExpected)} />
                        </Grid>
                    </Grid>
                    <Grid container spacing={2} alignItems="flex-end">
                        <Grid item xs={6}>
                            <Dropdown className={classes.dropdown} label="Classification" defaultValue={classification} data={classifications} onDropdownChange={(v) => onValueChange(v, setClassification)} />
                        </Grid>
                        <Grid item xs={6}>
                            <FormControlLabel label="Done?" control={<Switch color="primary" checked={isDone} onChange={(e) => onValueChange(e.target.checked, setIsDone)} />} />
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="secondary">
                        Cancel
                    </Button>
                    <Button onClick={handleSave} color="primary" autoFocus>
                        Save
                    </Button>
                </DialogActions>
            </Dialog>
        </React.Fragment>
    )
}