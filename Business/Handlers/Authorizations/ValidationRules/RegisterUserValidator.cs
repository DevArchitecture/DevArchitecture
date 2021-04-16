namespace Business.Handlers.Authorizations.ValidationRules
{
    using Business.Handlers.Authorizations.Commands;
    using FluentValidation;

    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(p => p.Password).Password();
        }
    }
}
