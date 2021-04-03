import ajaxService from './ajaxService'

const controller = 'Resource'

function getTasks(model) {
    return ajaxService.getSimple(`/${controller}/Sprints/${model.sprintId}/Tasks`);
}

function getSprints() {
    return ajaxService.getSimple(`/${controller}/Sprints`);
}

function getClassifications() {
    return ajaxService.getSimple(`/${controller}/Classifications`);
}

const resourceService = {
    getTasks: getTasks,
    getSprints: getSprints,
    getClassifications: getClassifications,
}

export default resourceService