namespace Business.Services.Authentication
{
    using Core.Entities.Concrete;
    using Core.Utilities.Security.Jwt;

    public class DArchToken : AccessToken
    {
        public string ExternalUserId { get; set; }
        public AuthenticationProviderType Provider { get; set; }
        public string OnBehalfOf { get; set; }
    }
}
