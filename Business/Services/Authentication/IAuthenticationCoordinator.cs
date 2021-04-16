namespace Business.Services.Authentication
{
    using Core.Entities.Concrete;

    public interface IAuthenticationCoordinator
    {
        IAuthenticationProvider SelectProvider(AuthenticationProviderType type);
    }
}