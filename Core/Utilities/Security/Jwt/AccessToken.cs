﻿namespace Core.Utilities.Security.Jwt
{
    using System;
    using System.Collections.Generic;

    public class AccessToken : IAccessToken
    {
        public List<string> Claims { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
