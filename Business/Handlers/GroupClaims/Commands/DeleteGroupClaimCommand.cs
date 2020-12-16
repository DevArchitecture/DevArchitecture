using Business.BusinessAspects;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GroupClaims.Commands
{
    [SecuredOperation]
    public class DeleteGroupClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteGroupClaimCommandHandler : IRequestHandler<DeleteGroupClaimCommand, IResult>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public DeleteGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            public async Task<IResult> Handle(DeleteGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var groupClaimToDelete = await _groupClaimRepository.GetAsync(x => x.GroupId == request.Id);

                _groupClaimRepository.Delete(groupClaimToDelete);
                await _groupClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.GroupClaimDeleted);
            }
        }
    }
}
