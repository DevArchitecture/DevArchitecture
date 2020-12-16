using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Commands
{
    [SecuredOperation]
    public class CreateUserClaimCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
        public class CreateUserClaimCommandHandler : IRequestHandler<CreateUserClaimCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimRepository;

            public CreateUserClaimCommandHandler(IUserClaimRepository userClaimRepository)
            {
                _userClaimRepository = userClaimRepository;
            }

            public async Task<IResult> Handle(CreateUserClaimCommand request, CancellationToken cancellationToken)
            {
                var userClaim = new UserClaim
                {
                    ClaimId = request.ClaimId,
                    UserId = request.UserId
                };
                _userClaimRepository.Add(userClaim);
                await _userClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.UserClaimCreated);
            }
        }
    }
}
