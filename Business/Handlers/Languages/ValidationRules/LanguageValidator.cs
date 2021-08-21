using Business.Handlers.Languages.Commands;
using FluentValidation;

namespace Business.Handlers.Languages.ValidationRules
{
    public class CreateLanguageValidator : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }

    public class UpdateLanguageValidator : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}