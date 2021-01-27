using Business.Helpers;
using Business.Services.Authentication.Model;
using Core.Entities;
using FluentValidation;
using Business.Constants;

namespace Business.Handlers.Authorizations
{
  
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(m => m.Password).NotEmpty()
              .When((i) => i.Provider != AuthenticationProviderType.Person);
            RuleFor(m => m.ExternalUserId).NotEmpty().Must((instance, value) =>
            {
                switch (instance.Provider)
                {
                    case AuthenticationProviderType.Person:
                        return value.IsCidValid();
                    case AuthenticationProviderType.Staff:
                        return true;
                    case AuthenticationProviderType.Agent:
                        break;
                    default:
                        break;
                }
                return false;
            })
            .WithMessage(Messages.InvalidCode)
            .OverridePropertyName(Messages.CID);
        }
    }

}
