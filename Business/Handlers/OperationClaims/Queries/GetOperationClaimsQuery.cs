using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OperationClaims.Queries
{
    [SecuredOperation]
    public class GetOperationClaimsQuery : IRequest<IDataResult<IEnumerable<OperationClaim>>>
    {
        public class GetOperationClaimsQueryHandler : IRequestHandler<GetOperationClaimsQuery, IDataResult<IEnumerable<OperationClaim>>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public GetOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<IDataResult<IEnumerable<OperationClaim>>> Handle(GetOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OperationClaim>>(await _operationClaimRepository.GetListAsync());
            }
        }
    }
}
