using System;

namespace Core.Utilities.Security.Jwt
{
    public class AccessToken : IAccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
