using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Queries
{
    [SecuredOperation]
    public class GetUserClaimLookupQuery : IRequest<IDataResult<IEnumerable<UserClaim>>>
    {
        public int UserId { get; set; }
        public class GetUserClaimQueryHandler : IRequestHandler<GetUserClaimLookupQuery, IDataResult<IEnumerable<UserClaim>>>
        {
            private readonly IUserClaimRepository _userClaimRepository;

            public GetUserClaimQueryHandler(IUserClaimRepository userClaimRepository)
            {
                _userClaimRepository = userClaimRepository;
            }

            public async Task<IDataResult<IEnumerable<UserClaim>>> Handle(GetUserClaimLookupQuery request, CancellationToken cancellationToken)
            {
                var userClaims = await _userClaimRepository.GetListAsync(x => x.UserId == request.UserId);

                return new SuccessDataResult<IEnumerable<UserClaim>>(userClaims.ToList());
            }
        }
    }
}

