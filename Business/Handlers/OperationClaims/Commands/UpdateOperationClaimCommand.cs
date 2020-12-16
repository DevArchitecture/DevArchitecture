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
    public class UpdateOperationClaimCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, IResult>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<IResult> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var isOperationClaimsExits = await _operationClaimRepository.GetAsync(u => u.Id == request.Id);
                isOperationClaimsExits.Alias = request.Alias;
                isOperationClaimsExits.Description = request.Description;

                _operationClaimRepository.Update(isOperationClaimsExits);
                await _operationClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.OperationClaimUpdated);
            }
        }
    }
}
