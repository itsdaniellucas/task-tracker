using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.Misc.Auth.JWT
{
    public class TokenResult
    {
        public string Token { get; set; }
        public string TokenExpiration { get; set; }

        public static TokenResult Create(string token, string expiration)
        {
            return new TokenResult()
            {
                Token = token,
                TokenExpiration = expiration
            };
        }
    }
}
