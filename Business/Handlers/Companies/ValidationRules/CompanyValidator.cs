
using Business.Handlers.Companies.Commands;
using FluentValidation;

namespace Business.Handlers.Companies.ValidationRules
{

    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FirmName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Phone2).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.TaxNo).NotEmpty();
            RuleFor(x => x.WebSite).NotEmpty();


        }
    }
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyValidator()
        {

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FirmName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Phone2).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.TaxNo).NotEmpty();
            RuleFor(x => x.WebSite).NotEmpty();


        }
    }
}