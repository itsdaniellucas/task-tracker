using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.SprintService.ActionInterfaces;
using TaskTracker.BLL.Services.SprintService.Validators;
using TaskTracker.BLL.Services.SprintService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.BLL.Services.SprintService.Actions
{
    public class AddSprintAction : IAddSprintAction
    {
        IContext _context;
        IRepository<Sprint> _sprintRepo;
        AddSprintValidator _validator;

        public AddSprintAction(IContext context, 
                                IRepository<Sprint> sprintRepo, 
                                AddSprintValidator validator)
        {
            _context = context;
            _sprintRepo = sprintRepo;
            _validator = validator;
        }
        public IServiceResult Execute(SprintServiceParameter parameter)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult> ExecuteAsync(SprintServiceParameter parameter)
        {
            var contextUserId = parameter.ContextUser.Id;
            var sprint = parameter.Sprint;

            var validation = await _validator.ValidateAsync(sprint);
            if (!validation.IsSuccessful)
                return ServiceResult.CreateError(validation.Error);

            var model = Mapper.Map<SprintVM, Sprint>(sprint);

            _sprintRepo.Insert(model, contextUserId);
            await _context.CommitAsync();

            return ServiceResult.CreateSuccess();
        }
    }
}
