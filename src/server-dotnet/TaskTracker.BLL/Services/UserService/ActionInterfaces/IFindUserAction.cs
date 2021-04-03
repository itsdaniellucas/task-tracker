using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.UserService.ViewModels;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.BLL.Services.UserService.ActionInterfaces
{
    public interface IFindUserAction : IServiceAction<IServiceResult<UserVM>, UserServiceParameter>
    {
    }
}
