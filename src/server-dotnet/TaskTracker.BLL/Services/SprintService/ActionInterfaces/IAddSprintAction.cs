using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.SprintService.ViewModels;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.BLL.Services.SprintService.ActionInterfaces
{
    public interface IAddSprintAction : IServiceAction<IServiceResult, SprintServiceParameter>
    {
    }
}
