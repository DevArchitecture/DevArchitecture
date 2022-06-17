using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.GroupClaims.Commands
{
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

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var list = request.ClaimIds.Select(x => new GroupClaim() { ClaimId = x, GroupId = request.GroupId });

                await _groupClaimRepository.BulkInsert(request.GroupId, list);
                await _groupClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}