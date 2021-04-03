import * as signalr from '@microsoft/signalr'
import state from '../state'
import actions from '../actions'
import appConfig from '../appConfig'

const baseUrl = `${appConfig.baseUrl}/SignalR`
let connection = null

function notifyTaskChanges(sprintId) {
    connection.invoke('SendTaskChanges', sprintId).catch(_ => _);
}

function onNotifiedTaskChanges(sprintId) {
    if(state.sprints.current == sprintId) {
        actions.tasks.fetch(sprintId);
    }
}

function initialize() {
    connection = new signalr.HubConnectionBuilder().withUrl(baseUrl).build();

    connection.on('ReceiveTaskChanges', onNotifiedTaskChanges);

    connection.start().catch(_ => _);
}


const signalRService = {
    notifyTaskChanges: notifyTaskChanges,
    onNotifiedTaskChanges: onNotifiedTaskChanges,
    initialize: initialize,
}

export default signalRService