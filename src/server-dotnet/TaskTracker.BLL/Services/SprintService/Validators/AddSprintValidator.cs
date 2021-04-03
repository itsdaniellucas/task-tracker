using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Misc;
using TaskTracker.BLL.Services.SprintService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.BLL.Services.SprintService.Validators
{
    public class AddSprintValidator : IValidator<SprintVM>
    {
        IRepository<Sprint> _sprintRepo;
        public AddSprintValidator(IRepository<Sprint> sprintRepo)
        {
            _sprintRepo = sprintRepo;
        }

        public IValidationResult Validate(SprintVM target)
        {
            throw new NotImplementedException();
        }

        public async Task<IValidationResult> ValidateAsync(SprintVM target)
        {
            var sprintExists = await _sprintRepo.FindActiveAsync(i => i.Name == target.Name);
            var sprintNameLimit = 64;
            var minLimit = 1;

            if (sprintExists != null)
                return ValidationResult.CreateError(Error.For(ErrorTypes.RecordExists, ErrorConstants.Sprint));
            else if(string.IsNullOrEmpty(target.Name) || target.Name.Length > sprintNameLimit)
                return ValidationResult.CreateError(Error.For(ErrorTypes.FieldOutOfRange, ErrorConstants.SprintName, minLimit.ToString(), sprintNameLimit.ToString()));
            return ValidationResult.CreateSuccess();
        }
    }
}
