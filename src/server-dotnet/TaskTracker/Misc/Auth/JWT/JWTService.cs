using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.BLL.Services.UserService.ViewModels;
using TaskTracker.Core.Architecture.BLL;

namespace TaskTracker.Misc.Auth.JWT
{
    public class JWTService : IJWTService
    {
        private string secretKey;
        private SecurityKey key;
        private IConfiguration _config;
        public JWTService(IConfiguration config)
        {
            _config = config;
            secretKey = _config["JWT:SecretKey"];

            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        public TokenResult GenerateToken(UserVM user)
        {
            var dateNow = DateTime.Now;
            var dateExpired = dateNow.AddMinutes(30);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            var issuer = _config["JWT:Issuer"];
            var audience = _config["JWT:Audience"];

            var claims = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
                new Claim(ClaimTypes.Role, user.Role.Name)
            }, "Custom");


            var descriptor = new SecurityTokenDescriptor()
            {
                Audience = audience,
                Issuer = issuer,
                Subject = claims,
                SigningCredentials = credentials,
                NotBefore = dateNow,
                IssuedAt = dateNow,
                Expires = dateExpired,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return TokenResult.Create(tokenString, dateExpired.ToUnixTimestamp().ToString());
        }
    }
}
