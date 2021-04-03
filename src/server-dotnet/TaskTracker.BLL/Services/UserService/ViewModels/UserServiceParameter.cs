using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.BLL.Services.UserService.ViewModels
{
    public class UserServiceParameter
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
}
