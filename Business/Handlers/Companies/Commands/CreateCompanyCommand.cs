
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Companies.ValidationRules;

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
            private readonly IMediator _mediator;
            public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMediator mediator)
            {
                _companyRepository = companyRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(CreateCompanyValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect()]            
            public async Task<IResult> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
            {
                var isThereCompanyRecord = _companyRepository.Query().Any(u => u.Name == request.Name);

                if (isThereCompanyRecord == true)
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