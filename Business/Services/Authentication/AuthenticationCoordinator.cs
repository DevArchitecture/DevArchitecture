using System;
using Core.Entities.Concrete;

namespace Business.Services.Authentication
{
    /// <summary>
    ///
    /// </summary>
    public class AuthenticationCoordinator(IServiceProvider serviceProvider) : IAuthenticationCoordinator
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public IAuthenticationProvider SelectProvider(AuthenticationProviderType type)
        {
            return type switch
            {
                AuthenticationProviderType.Person => (IAuthenticationProvider)_serviceProvider.GetService(
                    typeof(PersonAuthenticationProvider)),
                AuthenticationProviderType.Agent => (IAuthenticationProvider)_serviceProvider.GetService(
                    typeof(AgentAuthenticationProvider)),
                _ => throw new ApplicationException($"Authentication provider not found: {type}")
            };
        }
    }
}