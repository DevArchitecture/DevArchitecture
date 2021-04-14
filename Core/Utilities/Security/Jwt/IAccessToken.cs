namespace Core.Utilities.Security.Jwt
{
    using System;

    public interface IAccessToken
    {
        DateTime Expiration { get; set; }
        string Token { get; set; }
    }
}