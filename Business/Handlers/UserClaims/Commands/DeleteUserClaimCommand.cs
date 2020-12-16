using Business.BusinessAspects;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Commands
{
    [SecuredOperation]
    public class DeleteUserClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public class DeleteUserClaimCommandHandler : IRequestHandler<DeleteUserClaimCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimRepository;

            public DeleteUserClaimCommandHandler(IUserClaimRepository userClaimRepository)
            {
                _userClaimRepository = userClaimRepository;
            }

            public async Task<IResult> Handle(DeleteUserClaimCommand request, CancellationToken cancellationToken)
            {
                var entityToDelete = await _userClaimRepository.GetAsync(x => x.UserId == request.Id);

                _userClaimRepository.Delete(entityToDelete);
                await _userClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.UserClaimDeleted);
            }
        }
    }
}
