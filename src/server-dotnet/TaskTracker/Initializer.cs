using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.BLL.Misc;
using TaskTracker.BLL.Services.ResourceService.ActionInterfaces;
using TaskTracker.BLL.Services.ResourceService.Actions;
using TaskTracker.BLL.Services.ResourceService.ViewModels;
using TaskTracker.BLL.Services.SprintService.ActionInterfaces;
using TaskTracker.BLL.Services.SprintService.Actions;
using TaskTracker.BLL.Services.SprintService.Validators;
using TaskTracker.BLL.Services.SprintService.ViewModels;
using TaskTracker.BLL.Services.TaskService.ActionInterfaces;
using TaskTracker.BLL.Services.TaskService.Actions;
using TaskTracker.BLL.Services.TaskService.Validators;
using TaskTracker.BLL.Services.TaskService.ViewModels;
using TaskTracker.BLL.Services.UserService.ActionInterfaces;
using TaskTracker.BLL.Services.UserService.Actions;
using TaskTracker.BLL.Services.UserService.ViewModels;
using TaskTracker.Core;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Core.Architecture.DAL;
using TaskTracker.Core.Architecture.Models;
using TaskTracker.DAL;
using TaskTracker.DAL.Repositories;
using TaskTracker.Misc.Auth;
using TaskTracker.Misc.Auth.JWT;
using TaskTracker.Models;
using ErrorConfig = TaskTracker.Core.Error;
using TaskModel = TaskTracker.Models.Task;

namespace TaskTracker
{
    public class Initializer
    {

        public static void RegisterDependencies(IServiceCollection services)
        {
            // Context
            services.AddScoped<IContext, TaskTrackerContext>();

            // Repos
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Sprint>, SprintRepository>();
            services.AddScoped<IRepository<TaskModel>, TaskRepository>();
            services.AddScoped<IRepository<Classification>, ClassificationRepository>();

            // App Services
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IUserManager, UserManager>();

            // UserService
            services.AddScoped<IFindUserAction, FindUserAction>();

            // TaskService
            services.AddScoped<IAddTaskAction, AddTaskAction>();
            services.AddScoped<IModifyTaskAction, ModifyTaskAction>();
            services.AddScoped<IRemoveTaskAction, RemoveTaskAction>();
            services.AddScoped<AddOrModifyTaskValidator>();
            services.AddScoped<RemoveTaskValidator>();

            // SprintService
            services.AddScoped<IAddSprintAction, AddSprintAction>();
            services.AddScoped<AddSprintValidator>();

            // ResourceService
            services.AddScoped<IGetClassificationsAction, GetClassificationsAction>();
            services.AddScoped<IGetSprintsAction, GetSprintsAction>();
            services.AddScoped<IGetTasksAction, GetTasksAction>();
        }

        public static void ConstructDatabase(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var scopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = (scope.ServiceProvider.GetRequiredService<IContext>() as DbContext);
                context.Database.EnsureCreated();
            }
        }

        public static void RegisterErrors()
        {
            ErrorConfig.Register(ErrorTypes.APIFailure, "Associated API(s) for this feature have failed or have errors");
            ErrorConfig.Register(ErrorTypes.LoginFailure, "You are unauthorized to access this application.");
            ErrorConfig.Register(ErrorTypes.ConcurrentOperation, "Another operation with the same data is being done, please try again later.");
            ErrorConfig.Register(ErrorTypes.UserRoleInvalid, "You are unauthorized to perform this action due to role restrictions.");
            ErrorConfig.Register(ErrorTypes.RecordExists, "{0} already exists. {1}");
            ErrorConfig.Register(ErrorTypes.RecordNotExists, "{0} does not exist.");
            ErrorConfig.Register(ErrorTypes.RecordInvalid, "{0} is invalid. {1}");
            ErrorConfig.Register(ErrorTypes.FieldOutOfRange, "{0} is out of range. Must be between {1} and {2}");
            ErrorConfig.Register(ErrorTypes.FieldTooLong, "{0} is too long. Must be less than {1}");
            ErrorConfig.Register(ErrorTypes.FieldLessThanLimit, "{0} is smaller than the limit. Must be greater than {1}");
            ErrorConfig.Register(ErrorTypes.FieldGreaterThanLimit, "{0} is bigger than the limit. Must be less than {1}");
            ErrorConfig.Register(ErrorTypes.FieldRequired, "{0} is required");
        }

        public static void RegisterMappings()
        {
            Mapper.Register<IModelName, GenericViewModel>(i => new GenericViewModel()
            {
                Id = i.Id,
                Name = i.Name,
            });

            Mapper.Register<User, UserVM>(i => new UserVM()
            {
                Id = i.Id,
                Username = i.Username,
                Role = Mapper.Map<Role, RoleVM>(i.Role),
            });

            Mapper.Register<TaskModel, TaskVM>(i => new TaskVM()
            {
                Id = i.Id,
                SprintId = i.SprintId,
                ClassificationId = i.ClassificationId,
                Title = i.Title,
                Description = i.Description,
                ActualHours = i.ActualHours,
                ExpectedHours = i.ExpectedHours,
                IsCompleted = i.IsCompleted,
            });

            Mapper.Register<TaskVM, TaskModel>(i => new TaskModel()
            {
                Id = i.Id,
                SprintId = i.SprintId,
                ClassificationId = i.ClassificationId,
                Title = i.Title,
                Description = i.Description,
                ActualHours = i.ActualHours,
                ExpectedHours = i.ExpectedHours,
                IsCompleted = i.IsCompleted,
            });

            Mapper.Register<Role, RoleVM>(i => new RoleVM()
            {
                Id = i.Id,
                Name = i.Name,
            });

            Mapper.Register<SprintVM, Sprint>(i => new Sprint()
            {
                Id = i.Id,
                Name = i.Name,
            });
        }
    }
}
