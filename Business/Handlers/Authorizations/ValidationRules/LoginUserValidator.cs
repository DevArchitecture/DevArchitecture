using Business.Handlers.Authorizations.Queries;
using FluentValidation;

namespace Business.Handlers.Authorizations.ValidationRules;
public class LoginUserValidator : AbstractValidator<LoginUserQuery>
{
	public LoginUserValidator()
	{
		RuleFor(x => x.Email).EmailAddress();
		RuleFor(x => x.Password).Password();
	}
}
