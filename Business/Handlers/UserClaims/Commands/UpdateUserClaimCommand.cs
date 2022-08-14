using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserClaims.Commands;

public class UpdateUserClaimCommand : IRequest<IResult>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int[] ClaimIds { get; set; }


    public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, IResult>
    {
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly ICacheManager _cacheManager;

        public UpdateUserClaimCommandHandler(IUserClaimRepository userClaimRepository, ICacheManager cacheManager)
        {
            _userClaimRepository = userClaimRepository;
            _cacheManager = cacheManager;
        }

        [SecuredOperation]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
        {
            var userList = request.ClaimIds.Select(x => new UserClaim() { ClaimId = x, UserId = request.UserId });

            await _userClaimRepository.BulkInsert(request.UserId, userList);
            await _userClaimRepository.SaveChangesAsync();

            _cacheManager.Remove($"{CacheKeys.UserIdForClaim}={request.UserId}");

            return new SuccessResult(Messages.Updated);
        }
    }
}
