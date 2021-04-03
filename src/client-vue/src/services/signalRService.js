import Vue from 'vue'
import * as signalr from '@microsoft/signalr'
import state from '@/state'
import appConfig from '@/appConfig'

const signalRService = new Vue({
    data: () => ({
        baseUrl: `${appConfig.baseUrl}/SignalR`,
        connection: null,
    }),

    methods: {
        notifyTaskChanges(sprintId) {
            this.connection.invoke('SendTaskChanges', sprintId).catch(_ => _);
        },
        onNotifiedTaskChanges(sprintId) {
            if(state.sprints.current == sprintId) {
                state.tasks.fetch(sprintId);
            }
        },
        initialize() {
            this.connection = new signalr.HubConnectionBuilder().withUrl(this.baseUrl).build();

            this.connection.on('ReceiveTaskChanges', this.onNotifiedTaskChanges);

            this.connection.start().catch(_ => _);
        },
    },
})

export default signalRService