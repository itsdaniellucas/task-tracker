import ajaxService from './ajaxService'

const controller = 'Sprint'

function addSprint(model) {
    return ajaxService.post({
        url: `/${controller}/Add`,
        data: model,
    })
}

const sprintService = {
    addSprint: addSprint,
}

export default sprintService