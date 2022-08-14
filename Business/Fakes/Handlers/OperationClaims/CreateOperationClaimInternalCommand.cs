using AutoMapper;
using Business.Handlers.OperationClaims.Commands;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.OperationClaims.Commands.CreateOperationClaimCommand;

namespace Business.Fakes.Handlers.OperationClaims;

public class CreateOperationClaimInternalCommand : IRequest<IResult>
{
    public string ClaimName { get; set; }

    public class
        CreateOperationClaimInternalCommandHandler : IRequestHandler<CreateOperationClaimInternalCommand, IResult>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public CreateOperationClaimInternalCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateOperationClaimInternalCommand request, CancellationToken cancellationToken)
        {
            var handler = new CreateOperationClaimCommandHandler(_operationClaimRepository);
            return await handler.Handle(_mapper.Map<CreateOperationClaimCommand>(request), cancellationToken);
        }
    }
}
