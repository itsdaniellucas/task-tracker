using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.TaskService.ActionInterfaces;
using TaskTracker.BLL.Services.TaskService.Validators;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskModel = TaskTracker.Models.Task;

namespace TaskTracker.BLL.Services.TaskService.Actions
{
    public class RemoveTaskAction : IRemoveTaskAction
    {
        IContext _context;
        IRepository<TaskModel> _taskRepo;
        RemoveTaskValidator _validator;

        public RemoveTaskAction(IContext context, 
                                IRepository<TaskModel> taskRepo,
                                RemoveTaskValidator validator)
        {
            _context = context;
            _taskRepo = taskRepo;
            _validator = validator;
        }

        public IServiceResult Execute(TaskServiceParameter parameter)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult> ExecuteAsync(TaskServiceParameter parameter)
        {
            var contextUserId = parameter.ContextUser.Id;
            var task = parameter.Task;

            var validation = await _validator.ValidateAsync(task);
            if (!validation.IsSuccessful)
                return ServiceResult.CreateError(validation.Error);

            _taskRepo.Remove(validation.Data, contextUserId);
            await _context.CommitAsync();

            return ServiceResult.CreateSuccess();
        }
    }
}
