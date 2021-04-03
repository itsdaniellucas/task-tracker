using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.UserService.ActionInterfaces;
using TaskTracker.BLL.Services.UserService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Models;

namespace TaskTracker.BLL.Services.UserService.Actions
{
    public class FindUserAction : IFindUserAction
    {
        IRepository<User> _userRepo;

        public FindUserAction(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public IServiceResult<UserVM> Execute(UserServiceParameter parameter)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<UserVM>> ExecuteAsync(UserServiceParameter parameter)
        {
            var target = await _userRepo.FindActiveAsync(i => i.Username == parameter.Username && i.Password == parameter.HashedPassword);
            var result = Mapper.Map<User, UserVM>(target);
            return ServiceResult<UserVM>.CreateSuccess(result);
        }
    }
}
