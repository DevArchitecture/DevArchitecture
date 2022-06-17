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

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var isOperationClaimExists = await _operationClaimRepository.GetAsync(u => u.Id == request.Id);
                isOperationClaimExists.Alias = request.Alias;
                isOperationClaimExists.Description = request.Description;

                _operationClaimRepository.Update(isOperationClaimExists);
                await _operationClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}