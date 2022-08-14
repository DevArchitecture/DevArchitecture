using Business.Handlers.Authorizations.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.ValidationRules;
public class LoginUserValidator : AbstractValidator<LoginUserQuery>
{
	public LoginUserValidator()
	{
		RuleFor(x => x.Email).EmailAddress();
		RuleFor(x => x.Password).Password();
	}
}
