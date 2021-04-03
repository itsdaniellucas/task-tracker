using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.BLL.Services.UserService.ViewModels;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Misc.Auth;

namespace TaskTracker.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserManager _userManager;

        public LoginController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM login)
        {
            return Ok(await _userManager.Login(login));
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser()
        {
            var user = _userManager.GetContextUser(this.User);
            return Ok(ServiceResult<UserVM>.CreateSuccess(user));
        }
    }
}
