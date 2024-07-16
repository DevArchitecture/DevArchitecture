using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.GroupClaims.Queries
{
    public class GetGroupClaimQuery : IRequest<IDataResult<GroupClaim>>
    {
        public int Id { get; set; }

        public class GetGroupClaimQueryHandler(IGroupClaimRepository groupClaimRepository)
            : IRequestHandler<GetGroupClaimQuery, IDataResult<GroupClaim>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository = groupClaimRepository;

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<GroupClaim>> Handle(GetGroupClaimQuery request, CancellationToken cancellationToken) =>
                new SuccessDataResult<GroupClaim>(
                    await _groupClaimRepository.GetAsync(x => x.GroupId == request.Id));
        }
    }
}