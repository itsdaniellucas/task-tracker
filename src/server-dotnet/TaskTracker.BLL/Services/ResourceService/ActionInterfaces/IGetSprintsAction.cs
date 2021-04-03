using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.BLL.Services.ResourceService.ActionInterfaces
{
    public interface IGetSprintsAction : IServiceAction<IServiceResult<IEnumerable<GenericViewModel>>>
    {
    }
}
