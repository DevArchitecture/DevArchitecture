using System.Collections.Generic;
using System.Security.Claims;

namespace DevArchitecture.Specs.Helpers
{
    public static class ClaimsData
    {
        public static List<Claim> GetClaims()
        {
            return new()
            {
                new Claim("username", "deneme"),
                new Claim("email", "test@test.com"),
                new Claim("nameidentifier", "1"),
                new Claim("name", "GetUsersQuery")
            };
        }
    }
}
