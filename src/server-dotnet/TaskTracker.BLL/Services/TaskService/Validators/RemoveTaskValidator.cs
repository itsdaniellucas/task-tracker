using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Misc;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskModel = TaskTracker.Models.Task;

namespace TaskTracker.BLL.Services.TaskService.Validators
{
    public class RemoveTaskValidator : IValidator<TaskModel, TaskVM>
    {
        IRepository<TaskModel> _taskRepo;
        public RemoveTaskValidator(IRepository<TaskModel> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public IValidationResult<TaskModel> Validate(TaskVM target)
        {
            throw new NotImplementedException();
        }

        public async Task<IValidationResult<TaskModel>> ValidateAsync(TaskVM target)
        {
            var taskExists = await _taskRepo.FindActiveAsync(i => i.Id == target.Id);

            if (taskExists == null)
                return ValidationResult<TaskModel>.CreateError(Error.For(ErrorTypes.RecordNotExists, ErrorConstants.Task));
            return ValidationResult<TaskModel>.CreateSuccess(taskExists);
        }
    }
}
