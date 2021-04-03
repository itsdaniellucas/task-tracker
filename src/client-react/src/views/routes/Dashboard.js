import React, { useState, useEffect } from 'react';
import { Grid } from '@material-ui/core';
import TaskColumn from '../../components/TaskColumn';
import state from '../../state'
import utility from '../../utility'

export default function Dashboard(props) {

    const [backlog, setBacklog] = useState({});
    const [active, setActive] = useState({});
    const [closed, setClosed] = useState({});
    const [archived, setArchived] = useState({});
    const [backlogTasks, setBacklogTasks] = useState([]);
    const [activeTasks, setActiveTasks] = useState([]);
    const [closedTasks, setClosedTasks] = useState([]);
    const [archivedTasks, setArchivedTasks] = useState([]);

    useEffect(() => {
        const sub = state.classifications.values.subscribe(x => {
            const map = utility.toObjectMap(x, 'name');
            setBacklog(map['Backlog']);
            setActive(map['Active']);
            setClosed(map['Closed']);
            setArchived(map['Archived']);
        });

        return () => sub.unsubscribe();
    }, []);

    useEffect(() => {
        const sub = state.tasks.values.subscribe(x => {
            setBacklogTasks(x.filter(i => i.classificationId == backlog.id));
            setActiveTasks(x.filter(i => i.classificationId == active.id));
            setClosedTasks(x.filter(i => i.classificationId == closed.id));
            setArchivedTasks(x.filter(i => i.classificationId == archived.id));
        });

        return () => sub.unsubscribe();
    }, [backlog, active, closed, archived]);

    return (
        <Grid container spacing={4}>
            <Grid item xs={3}><TaskColumn tasks={backlogTasks} classification={backlog} next={active} /></Grid>
            <Grid item xs={3}><TaskColumn tasks={activeTasks} classification={active} next={closed} prev={backlog} /></Grid>
            <Grid item xs={3}><TaskColumn tasks={closedTasks} classification={closed} next={archived} prev={active} /></Grid>
            <Grid item xs={3}><TaskColumn tasks={archivedTasks} classification={archived} prev={closed} /></Grid>
        </Grid>
    )
}
