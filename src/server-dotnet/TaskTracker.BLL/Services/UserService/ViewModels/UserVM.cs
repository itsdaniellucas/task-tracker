using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.BLL.Services.ResourceService.ViewModels;

namespace TaskTracker.BLL.Services.UserService.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public RoleVM Role { get; set; }

        public override string ToString()
        {
            return $"{Username}";
        }
    }
}
