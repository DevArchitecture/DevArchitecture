using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.OperationClaims.Commands
{
    [SecuredOperation]
    public class CreateOperationClaimCommand : IRequest<IResult>
    {
        public string ClaimName { get; set; }

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, IResult>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository)
            {
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<IResult> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                if (IsClaimExists(request.ClaimName))
                    return new ErrorResult(Messages.OperationClaimExists);

                var operationClaim = new OperationClaim
                {
                    Name = request.ClaimName
                };
                _operationClaimRepository.Add(operationClaim);
                await _operationClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.OperationClaimAdded);
            }
            private bool IsClaimExists(string claimName)
            {
                return _operationClaimRepository.Query().Any(x => x.Name == claimName);
            }
        }
    }
}
