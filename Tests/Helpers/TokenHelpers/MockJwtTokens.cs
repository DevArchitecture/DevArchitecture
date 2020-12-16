using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SennedjemTests.Helpers.TokenHelpers
{
    public static class MockJwtTokens
    {
        public static string Issuer { get; } = "www.keremvaris.com";
        public static string Audience { get; } = "www.keremvaris.com";
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();
        private static string KeyString = "!z2x3C4v5B*_*!z2x3C4v5B*_*";

        static MockJwtTokens()
        {
            var s_key = Encoding.UTF8.GetBytes(KeyString);
            SecurityKey = new SymmetricSecurityKey(s_key);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

        }

        public static string GenerateJwtToken(IEnumerable<Claim> claims, double value = 2)
        {
            return s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, Audience, claims, DateTime.UtcNow, DateTime.UtcNow.AddSeconds(value), SigningCredentials));
        }
    }
}
