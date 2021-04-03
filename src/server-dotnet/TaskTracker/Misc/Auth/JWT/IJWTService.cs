using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.UserService.ViewModels;

namespace TaskTracker.Misc.Auth.JWT
{
    public interface IJWTService
    {
        TokenResult GenerateToken(UserVM user);
    }
}
