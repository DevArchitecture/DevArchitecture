using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserClaims.Queries;

public class GetUserClaimsQuery : IRequest<IDataResult<IEnumerable<UserClaim>>>
{
    public class
        GetUserClaimsQueryHandler : IRequestHandler<GetUserClaimsQuery, IDataResult<IEnumerable<UserClaim>>>
    {
        private readonly IUserClaimRepository _userClaimRepository;

        public GetUserClaimsQueryHandler(IUserClaimRepository userClaimRepository)
        {
            _userClaimRepository = userClaimRepository;
        }

        [SecuredOperation]
        [CacheAspect]
        [LogAspect]
        public async Task<IDataResult<IEnumerable<UserClaim>>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<UserClaim>>(await _userClaimRepository.GetListAsync());
        }
    }
}
