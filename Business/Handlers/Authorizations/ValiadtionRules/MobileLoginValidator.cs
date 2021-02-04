﻿using Business.Constants;
using Business.Services.Authentication.Model;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.Handlers.Authorizations.ValiadtionRules
{
    public class MobileLoginValidator : AbstractValidator<VerifyOtpCommand>
    {
        public MobileLoginValidator()
        {
            RuleFor(p => p.ExternalUserId).NotEmpty();
            RuleFor(m => m.Code).Must((instance, value) =>
            {
                switch (instance.Provider)
                {
                    case AuthenticationProviderType.Person:
                        return value > 0;
                    case AuthenticationProviderType.Staff:
                        return value > 0;
                    case AuthenticationProviderType.Agent:
                        return value == 0;
                }
                return false;
            })
            .WithMessage(Messages.InvalidCode);
        }
    }

}