using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.UserService.ViewModels;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.Misc.Auth
{
    public interface IUserManager
    {
        UserVM GetContextUser(ClaimsPrincipal principal);
        Task<IServiceResult<LoginResult>> Login(LoginVM login);
    }
}
