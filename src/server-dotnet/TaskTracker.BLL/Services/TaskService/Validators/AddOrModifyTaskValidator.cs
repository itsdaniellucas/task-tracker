using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Misc;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.BLL.Services.TaskService.Validators
{
    public class AddOrModifyTaskValidator : IValidator<TaskVM>
    {
        IRepository<Sprint> _sprintRepo;
        public AddOrModifyTaskValidator(IRepository<Sprint> sprintRepo)
        {
            _sprintRepo = sprintRepo;
        }
        public IValidationResult Validate(TaskVM target)
        {
            throw new NotImplementedException();
        }

        public async Task<IValidationResult> ValidateAsync(TaskVM target)
        {
            var sprintExists = await _sprintRepo.FindActiveAsync(i => i.Id == target.SprintId);
            var taskTitleLimit = 64;
            var taskDescLimit = 256;
            var minLimit = 1;

            if(sprintExists == null)
                return ValidationResult.CreateError(Error.For(ErrorTypes.RecordNotExists, ErrorConstants.Sprint));
            if (string.IsNullOrEmpty(target.Title) || target.Title.Length > taskTitleLimit)
                return ValidationResult.CreateError(Error.For(ErrorTypes.FieldOutOfRange, ErrorConstants.TaskName, minLimit.ToString(), taskTitleLimit.ToString()));
            else if (string.IsNullOrEmpty(target.Description) || target.Description.Length > taskDescLimit)
                return ValidationResult.CreateError(Error.For(ErrorTypes.FieldOutOfRange, ErrorConstants.TaskDescription, minLimit.ToString(), taskDescLimit.ToString()));

            return ValidationResult.CreateSuccess();
        }
    }
}
