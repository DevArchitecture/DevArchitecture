using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GroupClaims.Queries
{
    [SecuredOperation]
    public class GetGroupClaimsQuery : IRequest<IDataResult<IEnumerable<GroupClaim>>>
    {

        public class GetGroupClaimsQueryHandler : IRequestHandler<GetGroupClaimsQuery, IDataResult<IEnumerable<GroupClaim>>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public GetGroupClaimsQueryHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            public async Task<IDataResult<IEnumerable<GroupClaim>>> Handle(GetGroupClaimsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GroupClaim>>(await _groupClaimRepository.GetListAsync());
            }
        }
    }
}
