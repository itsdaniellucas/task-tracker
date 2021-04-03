using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.ResourceService.ActionInterfaces;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.BLL.Services.ResourceService.Actions
{
    public class GetSprintsAction : GetAllGenericViewModelAction<Sprint>, IGetSprintsAction
    {
        public GetSprintsAction(IRepository<Sprint> repository) : base(repository)
        {
        }
    }
}
