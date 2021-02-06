using Core.Utilities.Security.Jwt;
using Core.Entities.Concrete;

namespace Business.Services.Authentication
{
    public class DArchToken : AccessToken
    {
        public string ExternalUserId { get; set; }
        public AuthenticationProviderType Provider { get; set; }
        public string OnBehalfOf { get; set; }
    }
}
