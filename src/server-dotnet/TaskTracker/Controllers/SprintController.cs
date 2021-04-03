using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Services.SprintService.ActionInterfaces;
using TaskTracker.BLL.Services.SprintService.ViewModels;
using TaskTracker.Misc.Auth;

namespace TaskTracker.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Policy = Policy.Admin)]
    public class SprintController : ControllerBase
    {
        IAddSprintAction _addSprint;
        IUserManager _userManager;

        public SprintController(IUserManager userManager, IAddSprintAction addSprint)
        {
            _userManager = userManager;
            _addSprint = addSprint;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddSprint(SprintVM model)
        {
            var user = _userManager.GetContextUser(this.User);
            var svcParam = new SprintServiceParameter()
            {
                Sprint = model,
                ContextUser = user,
            };

            return Ok(await _addSprint.ExecuteAsync(svcParam));
        }

    }
}
