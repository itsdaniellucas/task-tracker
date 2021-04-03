import Vue from 'vue'
import AjaxService from '@/services/ajaxService'

const sprintService = new Vue({
    data: () => ({
        controller: 'Sprint'
    }),

    methods: {
        addSprint(model) {
            return AjaxService.post({
                url: `/${this.controller}/Add`,
                data: model,
            });
        },
    }
})

export default sprintService