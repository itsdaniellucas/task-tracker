import Vue from 'vue'
import AjaxService from '@/services/ajaxService'

const taskService = new Vue({
    data: () => ({
        controller: 'Task'
    }),

    methods: {
        addTask(model) {
            return AjaxService.post({
                url: `/${this.controller}/Add`,
                data: model,
            });
        },
        modifyTask(model) {
            return AjaxService.post({
                url: `/${this.controller}/Modify`,
                data: model,
            });
        },
        removeTask(model) {
            return AjaxService.post({
                url: `/${this.controller}/Remove`,
                data: model,
            });
        }
    }
})

export default taskService