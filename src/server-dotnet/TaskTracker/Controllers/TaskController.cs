using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Services.TaskService.ActionInterfaces;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.Misc.Auth;

namespace TaskTracker.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        IUserManager _userManager;
        IAddTaskAction _addTask;
        IModifyTaskAction _modifyTask;
        IRemoveTaskAction _removeTask;

        public TaskController(IUserManager userManager,
                                IAddTaskAction addTask,
                                IModifyTaskAction modifyTask,
                                IRemoveTaskAction removeTask)
        {
            _userManager = userManager;
            _addTask = addTask;
            _modifyTask = modifyTask;
            _removeTask = removeTask;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddTask(TaskVM model)
        {
            var user = _userManager.GetContextUser(this.User);
            var svcParam = new TaskServiceParameter()
            {
                Task = model,
                ContextUser = user,
            };

            return Ok(await _addTask.ExecuteAsync(svcParam));
        }

        [HttpPost]
        [Route("Modify")]
        public async Task<IActionResult> ModifyTask(TaskVM model)
        {
            var user = _userManager.GetContextUser(this.User);
            var svcParam = new TaskServiceParameter()
            {
                Task = model,
                ContextUser = user,
            };

            return Ok(await _modifyTask.ExecuteAsync(svcParam));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveTask(TaskVM model)
        {
            var user = _userManager.GetContextUser(this.User);
            var svcParam = new TaskServiceParameter()
            {
                Task = model,
                ContextUser = user,
            };

            return Ok(await _removeTask.ExecuteAsync(svcParam));
        }
    }
}
