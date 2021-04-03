using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.BLL.Services.TaskService.ActionInterfaces
{
    public interface IModifyTaskAction : IServiceAction<IServiceResult, TaskServiceParameter>
    {
    }
}
