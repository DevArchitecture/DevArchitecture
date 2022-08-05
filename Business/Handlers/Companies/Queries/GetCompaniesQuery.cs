
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Business.Helpers;
using Core.Entities.Concrete;

namespace Business.Handlers.Companies.Queries
{

    public class GetCompaniesQuery : IRequest<IDataResult<IEnumerable<Company>>>
    {
        public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, IDataResult<IEnumerable<Company>>>
        {
            private readonly ICompanyRepository _companyRepository;
            private readonly IMediator _mediator;

            public GetCompaniesQueryHandler(ICompanyRepository companyRepository, IMediator mediator)
            {
                _companyRepository = companyRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect()]
            public async Task<IDataResult<IEnumerable<Company>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
            {
                var tenant = await _mediator.Send(new GetTenantQuery(), cancellationToken);
                if (tenant != null && tenant.Data.UserId == 1)
                {
                    return new SuccessDataResult<IEnumerable<Company>>(await _companyRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Company>>(await _companyRepository.GetListAsync(x => x.TenantId == tenant.Data.TenantId));
            }
        }
    }
}