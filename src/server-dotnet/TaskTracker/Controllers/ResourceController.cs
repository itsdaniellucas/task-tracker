using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Services.ResourceService.ActionInterfaces;
using TaskTracker.BLL.Services.ResourceService.ViewModels;

namespace TaskTracker.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        IGetClassificationsAction _getClassifications;
        IGetSprintsAction _getSprints;
        IGetTasksAction _getTasks;

        public ResourceController(IGetClassificationsAction getClassifications,
                                    IGetSprintsAction getSprints,
                                    IGetTasksAction getTasks)
        {
            _getClassifications = getClassifications;
            _getSprints = getSprints;
            _getTasks = getTasks;
        }           
        
        [HttpGet]
        [Route("Sprints")]
        public async Task<IActionResult> GetSprints()
        {
            return Ok(await _getSprints.ExecuteAsync());
        }
        
        [HttpGet]
        [Route("Classifications")]
        public async Task<IActionResult> GetClassifications()
        {
            return Ok(await _getClassifications.ExecuteAsync());
        }

        [HttpGet]
        [Route("Sprints/{sprintId}/Tasks")]
        public async Task<IActionResult> GetTasks(int sprintId)
        {
            var svcParam = new ResourceServiceParameter()
            {
                SprintId = sprintId
            };

            return Ok(await _getTasks.ExecuteAsync(svcParam));
        }

    }
}
