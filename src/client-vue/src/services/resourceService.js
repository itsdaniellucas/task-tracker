import Vue from 'vue'
import AjaxService from '@/services/ajaxService'

const resourceService = new Vue({
    data: () => ({
        controller: 'Resource'
    }),

    methods: {
        getTasks(model) {
            return AjaxService.getSimple(`/${this.controller}/Sprints/${model.sprintId}/Tasks`);
        },
        getSprints() {
            return AjaxService.getSimple(`/${this.controller}/Sprints`);
        },
        getClassifications() {
            return AjaxService.getSimple(`/${this.controller}/Classifications`);
        },
    }
})

export default resourceService