using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.GroupClaims.Commands
{
    [SecuredOperation]
    public class UpdateGroupClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int[] ClaimIds { get; set; }
        public class UpdateGroupClaimCommandHandler : IRequestHandler<UpdateGroupClaimCommand, IResult>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;

            public UpdateGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository)
            {
                _groupClaimRepository = groupClaimRepository;
            }

            public async Task<IResult> Handle(UpdateGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var list = request.ClaimIds.Select(x => new GroupClaim() { ClaimId = x, GroupId = request.GroupId });

                await _groupClaimRepository.BulkInsert(request.GroupId, list);
                await _groupClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.GroupClaimUpdated);
            }
        }
    }
}
