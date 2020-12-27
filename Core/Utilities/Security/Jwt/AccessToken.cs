using System;
using System.Collections.Generic;

namespace Core.Utilities.Security.Jwt
{
    public class AccessToken : IAccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; } 
        public Dictionary<string,string> Translates { get; set; }
    }
}
