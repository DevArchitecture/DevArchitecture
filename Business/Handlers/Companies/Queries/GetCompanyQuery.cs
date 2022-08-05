
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

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
