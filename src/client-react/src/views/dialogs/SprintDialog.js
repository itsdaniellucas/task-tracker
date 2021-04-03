import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Divider, Button, IconButton, Dialog, DialogActions, DialogTitle, DialogContent } from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add'
import FingerprintIcon from '@material-ui/icons/Fingerprint';
import TextBox from '../../components/TextBox'

const useStyles = makeStyles((theme) => ({
    content: {
        paddingTop: theme.spacing(2),
        paddingBottom: theme.spacing(2),
    }
}));

export default function SprintDialog(props) {
    const classes = useStyles();
    const [visible, setVisible] = useState(false);
    const [sprint, setSprint] = useState('');

    const handleOpen = () => {
        setVisible(true);
    }

    const handleClose = () => {
        setVisible(false);
    }

    const onSprintChange = (value) => {
        setSprint(value);
    }

    const handleSave = () => {
        if(props.onSave) {
            props.onSave(sprint);
        }
        handleClose();
    }

    return (
        <React.Fragment>
            <IconButton className={props.className} onClick={handleOpen} disabled={props.disabled}>
                <AddIcon />
            </IconButton>
            <Dialog open={visible}
                    onClose={handleClose}>
                <DialogTitle>Add Sprint</DialogTitle>
                <Divider />
                <DialogContent className={classes.content}>
                    <TextBox icon={<FingerprintIcon />} label="Sprint" onTextBoxChange={onSprintChange} />
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