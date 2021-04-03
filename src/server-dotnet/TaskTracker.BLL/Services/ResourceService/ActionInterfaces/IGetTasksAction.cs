using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.ResourceService.ViewModels;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.BLL.Services.ResourceService.ActionInterfaces
{
    public interface IGetTasksAction : IServiceAction<IServiceResult<IEnumerable<TaskVM>>, ResourceServiceParameter>
    {
    }
}
