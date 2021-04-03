using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.BLL.Services.ResourceService.ActionInterfaces
{
    public interface IGetClassificationsAction : IServiceAction<IServiceResult<IEnumerable<GenericViewModel>>>
    {
    }
}
