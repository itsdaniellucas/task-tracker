import { BehaviorSubject } from 'rxjs'

const state = {
    tasks: {
        values: new BehaviorSubject([]),
    },
    classifications: {
        values: new BehaviorSubject([]),
    },
    sprints: {
        values: new BehaviorSubject([]),
        current: 0,
    },
    user: {
        value: new BehaviorSubject(null),
    },
    alert: {
        config: new BehaviorSubject({
            visible: false,
            isProgress: true,
            text: 'Saving changes made...',
            severity: 'info',
            timeout: 5000,
            timeoutFn: null,
            type: 'saving',
        })
    },
    isFetching: new BehaviorSubject(false),
}

export default state
