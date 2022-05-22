using Business.Handlers.Authorizations.Commands;
using FluentValidation;

namespace Business.Handlers.Authorizations.ValidationRules;
internal class ExternalLoginUserValidator : AbstractValidator<ExternalLoginUserCommand>
{
    public ExternalLoginUserValidator()
    {
        RuleFor(p => p.Token).NotNull();
        RuleFor(p => p.Provider).NotNull();
    }
}
