using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.UserService.ViewModels;

namespace TaskTracker.Misc.Auth
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string TokenExpiration { get; set; }
        public string Error { get; set; }
        public UserVM User { get; set; }

        public static LoginResult Create(UserVM user, string token, string expiration, string error)
        {
            return new LoginResult()
            {
                User = user,
                Token = token,
                TokenExpiration = expiration,
                Error = error
            };
        }
    }
}
