using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.UserService.ViewModels;

namespace TaskTracker.BLL.Services.TaskService.ViewModels
{
    public class TaskServiceParameter
    {
        public TaskVM Task { get; set; }
        public UserVM ContextUser { get; set; }
    }
}
