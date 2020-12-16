using Business.BusinessAspects;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OperationClaims.Commands
{
    [SecuredOperation]
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

            public async Task<IResult> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var claimToDelete = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);
                _operationClaimRepository.Delete(claimToDelete);
                await _operationClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.OperationClaimDeleted);
            }
        }
    }
}
