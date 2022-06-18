using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.Queries;
using FluentValidation;

namespace Business.Handlers.Authorizations.ValidationRules;
internal class ExternalLoginUserValidator : AbstractValidator<ExternalLoginUserQuery>
{
    public ExternalLoginUserValidator()
    {
        RuleFor(p => p.Token).NotNull();
        RuleFor(p => p.Provider).NotNull();
    }
}
