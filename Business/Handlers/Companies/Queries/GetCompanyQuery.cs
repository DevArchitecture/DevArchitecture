
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;

namespace Business.Handlers.Companies.Queries
{
    public class GetCompanyQuery : IRequest<IDataResult<Company>>
    {
        public int Id { get; set; }

        public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, IDataResult<Company>>
        {
            private readonly ICompanyRepository _companyRepository;
            private readonly IMediator _mediator;

            public GetCompanyQueryHandler(ICompanyRepository companyRepository, IMediator mediator)
            {
                _companyRepository = companyRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect()]          
            public async Task<IDataResult<Company>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
            {
                var company = await _companyRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Company>(company);
            }
        }
    }
}
