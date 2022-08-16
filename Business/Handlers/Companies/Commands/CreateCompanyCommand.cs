
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Companies.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Companies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCompanyCommand : IRequest<IResult>
    {

        public int TenantId { get; set; }
        public string Name { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string TaxNo { get; set; }
        public string WebSite { get; set; }


        public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, IResult>
        {
            private readonly ICompanyRepository _companyRepository;
            public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
            {
                _companyRepository = companyRepository;
            }

            [SecuredOperation]
            [ValidationAspect(typeof(CreateCompanyValidator))]
            [CacheRemoveAspect]
            [LogAspect]
            public async Task<IResult> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
            {
                var isThereCompanyRecord = _companyRepository.Query().Any(u => u.Name == request.Name);

                if (isThereCompanyRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCompany = new Company
                {
                    TenantId = request.TenantId,
                    Name = request.Name,
                    FirmName = request.FirmName,
                    Address = request.Address,
                    Phone = request.Phone,
                    Phone2 = request.Phone2,
                    Email = request.Email,
                    TaxNo = request.TaxNo,
                    WebSite = request.WebSite,
                };

                _companyRepository.Add(addedCompany);
                await _companyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}