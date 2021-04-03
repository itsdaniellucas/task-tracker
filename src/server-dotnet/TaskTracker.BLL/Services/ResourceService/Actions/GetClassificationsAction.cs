using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.ResourceService.ActionInterfaces;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.BLL.Services.ResourceService.Actions
{
    public class GetClassificationsAction : GetAllGenericViewModelAction<Classification>, IGetClassificationsAction
    {
        public GetClassificationsAction(IRepository<Classification> repository) : base(repository)
        {
        }
    }
}
