namespace Core.Utilities.Security.Encyption
{
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public static class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
