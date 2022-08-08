using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.Companies
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCompanyInternalCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string TaxNo { get; set; }
        public string WebSite { get; set; }



        public class CreateCompanyInternalCommandHandler : IRequestHandler<CreateCompanyInternalCommand, IResult>
        {
            private readonly ICompanyRepository _companyRepository;
            private readonly IMediator _mediator;
            public CreateCompanyInternalCommandHandler(ICompanyRepository companyRepository, IMediator mediator)
            {
                _companyRepository = companyRepository;
                _mediator = mediator;
            }

            //[ValidationAspect(typeof(CreateCompanyValidator), Priority = 1)]
            //[CacheRemoveAspect("Get")]
            //[LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCompanyInternalCommand request, CancellationToken cancellationToken)
            {
                var isThereCompanyRecord = _companyRepository.Query().Any(u => u.Name == request.Name);

                if (isThereCompanyRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCompany = new Company
                {
                    Name = request.Name,
                    FirmName = request.FirmName,
                    Address = request.Address,
                    Phone = request.Phone,
                    Phone2 = request.Phone2,
                    Email = request.Email,
                    TaxNo = request.TaxNo,
                    WebSite = request.WebSite
                };

                _companyRepository.Add(addedCompany);
                await _companyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}