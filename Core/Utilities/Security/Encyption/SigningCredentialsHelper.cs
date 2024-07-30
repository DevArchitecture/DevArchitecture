using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encyption
{
    public static class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) =>
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}