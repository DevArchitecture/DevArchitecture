using FluentValidation;

namespace Business.Handlers.Authorizations.Commands
{
 
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(p => p.Password).Password();
        }
    }
}
