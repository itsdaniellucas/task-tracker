import ajaxService from './ajaxService'

const controller = 'Task'

function addTask(model) {
    return ajaxService.post({
        url: `/${controller}/Add`,
        data: model,
    })
}

function modifyTask(model) {
    return ajaxService.post({
        url: `/${controller}/Modify`,
        data: model,
    })
}

function removeTask(model) {
    return ajaxService.post({
        url: `/${controller}/Remove`,
        data: model,
    })
}

const taskService = {
    addTask: addTask,
    modifyTask: modifyTask,
    removeTask: removeTask,
}

export default taskService