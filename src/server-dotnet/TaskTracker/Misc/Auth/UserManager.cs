using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.UserService.ActionInterfaces;
using TaskTracker.BLL.Services.UserService.ViewModels;
using TaskTracker.Core.Architecture.BLL;
using TaskTracker.Misc.Auth.JWT;

namespace TaskTracker.Misc.Auth
{
    public class UserManager : IUserManager
    {
        IFindUserAction _findUser;
        IJWTService _jwtService;

        public UserManager(IFindUserAction findUser, IJWTService jwtService)
        {
            _findUser = findUser;
            _jwtService = jwtService;
        }

        public UserVM GetContextUser(ClaimsPrincipal principal)
        {
            var userData = principal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.UserData);

            if (userData == null)
                return null;

            var user = JsonConvert.DeserializeObject<UserVM>(userData.Value);
            return user;
        }

        public async Task<IServiceResult<LoginResult>> Login(LoginVM login)
        {
            var findUserResult = await _findUser.ExecuteAsync(new UserServiceParameter()
            {
                Username = login.Username,
                HashedPassword = GetSHA256Hash(login.Password)
            });

            var user = findUserResult.Data;
            var tokenResult = new TokenResult();
            var error = string.Empty;

            if (user == null)
                error = "Incorrect username or password";
            else
                tokenResult = _jwtService.GenerateToken(user);

            var loginResult = LoginResult.Create(user, tokenResult.Token, tokenResult.TokenExpiration, error);

            return ServiceResult<LoginResult>.CreateSuccess(loginResult);
        }

        private static string GetSHA256Hash(string value)
        {
            StringBuilder sb = new StringBuilder();

            using(var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
