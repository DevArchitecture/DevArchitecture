﻿using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.UserClaims;

/// <summary>
/// For Internal Use Only,
/// Registers All Existing Operation Claims To Given User
/// </summary>
public class CreateUserClaimsInternalCommand : IRequest<IResult>
{
    public int UserId { get; set; }
    public IEnumerable<OperationClaim> OperationClaims { get; set; }

    public class CreateUserClaimsInternalCommandHandler : IRequestHandler<CreateUserClaimsInternalCommand, IResult>
    {
        private readonly IUserClaimRepository _userClaimsRepository;

        public CreateUserClaimsInternalCommandHandler(IUserClaimRepository userClaimsRepository)
        {
            _userClaimsRepository = userClaimsRepository;
        }

        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateUserClaimsInternalCommand request, CancellationToken cancellationToken)
        {
            foreach (var claimId in request.OperationClaims.Select(claim => claim.Id))
            {
                if (await DoesClaimExistsForUser(new UserClaim { ClaimId = claimId, UserId = request.UserId }))
                {
                    continue;
                }

                _userClaimsRepository.Add(new UserClaim
                {
                    ClaimId = claimId,
                    UserId = request.UserId
                });
            }

            await _userClaimsRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Added);
        }

        private async Task<bool> DoesClaimExistsForUser(UserClaim userClaim)
        {
            return (await _userClaimsRepository.GetAsync(x =>
                x.UserId == userClaim.UserId && x.ClaimId == userClaim.ClaimId)) is { };
        }
    }
}
