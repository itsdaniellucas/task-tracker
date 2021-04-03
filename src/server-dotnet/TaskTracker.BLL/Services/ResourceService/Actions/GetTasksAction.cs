using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.ResourceService.ActionInterfaces;
using TaskTracker.BLL.Services.ResourceService.ViewModels;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskModel = TaskTracker.Models.Task;

namespace TaskTracker.BLL.Services.ResourceService.Actions
{
    public class GetTasksAction : IGetTasksAction
    {
        IRepository<TaskModel> _taskRepo;

        public GetTasksAction(IRepository<TaskModel> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public IServiceResult<IEnumerable<TaskVM>> Execute(ResourceServiceParameter parameter)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<IEnumerable<TaskVM>>> ExecuteAsync(ResourceServiceParameter parameter)
        {
            var tasks = await _taskRepo.FindAllActiveAsync(i => i.SprintId == parameter.SprintId);
            var result = Mapper.Map<TaskModel, TaskVM>(tasks);
            return ServiceResult<IEnumerable<TaskVM>>.CreateSuccess(result);
        }
    }
}
