using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.OperationClaims.Queries;

public class GetOperationClaimsQuery : IRequest<IDataResult<IEnumerable<OperationClaim>>>
{
    public class
        GetOperationClaimsQueryHandler : IRequestHandler<GetOperationClaimsQuery,
            IDataResult<IEnumerable<OperationClaim>>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public GetOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        [SecuredOperation(Priority = 1)]
        [CacheAspect(10)]
        [LogAspect()]
        public async Task<IDataResult<IEnumerable<OperationClaim>>> Handle(GetOperationClaimsQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<OperationClaim>>(
                await _operationClaimRepository.GetListAsync());
        }
    }
}
