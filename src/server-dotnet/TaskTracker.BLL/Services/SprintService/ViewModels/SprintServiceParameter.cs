using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.UserService.ViewModels;

namespace TaskTracker.BLL.Services.SprintService.ViewModels
{
    public class SprintServiceParameter
    {
        public SprintVM Sprint { get; set; }
        public UserVM ContextUser { get; set; }
    }
}
