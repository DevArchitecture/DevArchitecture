using Business.Constants;
using FluentValidation;

namespace Business.Handlers.Authorizations.ValidationRules
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
        {
            var options = ruleBuilder
                .NotEmpty().WithMessage(Messages.PasswordEmpty)
                .MinimumLength(minimumLength).WithMessage(Messages.PasswordLength)
                .Matches("[A-Z]").WithMessage(Messages.PasswordUppercaseLetter)
                .Matches("[a-z]").WithMessage(Messages.PasswordLowercaseLetter)
                .Matches("[0-9]").WithMessage(Messages.PasswordDigit)
                .Matches("[^a-zA-Z0-9]").WithMessage(Messages.PasswordSpecialCharacter);
            return options;
        }
    }
}