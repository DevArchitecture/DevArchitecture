using Core.Entities;
using System;

namespace Business.Services.Authentication
{
    /// <summary>
    /// Çok hızlı yazmak zorunda kaldım, refactor gerekecek.
    /// </summary>
    public class AuthenticationCoordinator : IAuthenticationCoordinator
    {
        private readonly IServiceProvider _serviceProvider;

        public AuthenticationCoordinator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public IAuthenticationProvider SelectProvider(AuthenticationProviderType type)
        {
            return type switch
            {
                AuthenticationProviderType.Person => (IAuthenticationProvider)_serviceProvider.GetService(typeof(PersonAuthenticationProvider)),
                AuthenticationProviderType.Agent => (IAuthenticationProvider)_serviceProvider.GetService(typeof(AgentAuthenticationProvider)),
                _ => throw new ApplicationException($"Authentication provider not found: {type}")
            };
        }
    }

}
