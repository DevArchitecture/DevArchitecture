using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.OperationClaims.Commands
{
    public class DeleteOperationClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }


        public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, IResult>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var claimToDelete = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);
                _operationClaimRepository.Delete(claimToDelete);
                await _operationClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}