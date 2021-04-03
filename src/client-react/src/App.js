import React, { useEffect, useState, useCallback } from 'react'
import './App.css';
import { makeStyles } from '@material-ui/core/styles';
import { AppBar, Toolbar, Typography, Button, LinearProgress } from '@material-ui/core'
import { Link } from 'react-router-dom'
import Dropdown from './components/Dropdown'
import LoginDialog from './views/dialogs/LoginDialog'
import SprintDialog from './views/dialogs/SprintDialog'
import RouteConfig from './RouteConfig'
import AlertBox from './views/content/AlertBox'
import state from './state'
import actions from './actions'
import loginService from './services/loginService'
import signalRService from './services/signalRService'

const useStyles = makeStyles((theme) => ({  
  login: {
    marginRight: theme.spacing(4),
    marginLeft: theme.spacing(2),
  },
  dropdown: {
    marginRight: theme.spacing(2),
    minWidth: 200,
  },
  dropdownButton: {
    marginRight: theme.spacing(2),
  },
  appBar: {
    position: 'relative',
  },
  layout: {
    width: 'auto',
    padding: theme.spacing(4),
  },
  title: {
    flexGrow: 1,
  },
}));

function App() {
  const classes = useStyles();

  const [sprints, setSprints] = useState([]);
  const [currentSprint, setCurrentSprint] = useState(state.sprints.current);
  const [isFetching, setIsFetching] = useState(false);
  const [user, setUser] = useState(null);

  const onSprintChange = useCallback((value) => {
    state.sprints.current = value;
    setCurrentSprint(value);
  }, [])

  useEffect(() => {
    signalRService.initialize();

    if(loginService.isLoggedIn()) {
      loginService.getUser(true).then(u => {
        state.user.value.next(u);
      });
    } else {
      loginService.logout(true);
    }
    

    if(state.classifications.values.value.length == 0) {
      actions.classifications.fetch();
    }

    if(state.sprints.values.value.length == 0) {
      actions.sprints.fetch().then(() => {
        if(state.sprints.values.value.length != 0) {
          const firstSprint = state.sprints.values.value[0].id;
          onSprintChange(firstSprint);
          return actions.tasks.fetch(state.sprints.current);
        }
      });
    } else {
      const firstSprint = state.sprints.values.value[0].id;
      onSprintChange(firstSprint);
    }
  }, [onSprintChange]);

  useEffect(() => {
    actions.tasks.fetch(currentSprint);
  }, [currentSprint]);

  useEffect(() => {
    const sub = state.sprints.values.subscribe(x => {
      setSprints(x);
    })

    return () => sub.unsubscribe();
  }, []);

  useEffect(() => {
    const sub = state.isFetching.subscribe(x => {
      setIsFetching(x);
    })

    return () => sub.unsubscribe();
  }, [])

  useEffect(() => {
    const sub = state.user.value.subscribe(x => {
        setUser(x);
    });

    return () => sub.unsubscribe();
  }, []);

  

  const onSprintSave = useCallback((sprint) => {
    actions.sprints.addSprint(sprint).then(() => {
      return actions.sprints.fetch();
    })
  }, [])

  const onLogin = useCallback((login) => {
    loginService.login({
      Username: login.username,
      Password: login.password
    })
  }, [])

  const onLogout = useCallback(() => {
    loginService.logout();
  }, [])

  return (
    <div>
      <AppBar className={classes.appBar} color="transparent">
        <Toolbar>
          <LoginDialog className={classes.login} onLogin={onLogin} onLogout={onLogout} />
          <Typography variant="h6" className={classes.title}>
            <Link to="/"><Button>Dashboard</Button></Link>
            <Link to="/stats"><Button>Statistics</Button></Link>
          </Typography>
          <Dropdown className={classes.dropdown} label="Sprint" defaultValue={currentSprint} data={sprints} onDropdownChange={onSprintChange} />
          <SprintDialog className={classes.dropdownButton} disabled={user == null || user.Role.Name != 'Admin'} onSave={onSprintSave} />
        </Toolbar>
      </AppBar>
      { isFetching ? <LinearProgress /> : null }
      <main className={classes.layout}>
        <RouteConfig />
        <AlertBox />
      </main>
    </div>
  );
}

export default App;
