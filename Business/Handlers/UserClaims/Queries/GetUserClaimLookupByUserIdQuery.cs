using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserClaims.Queries
{
    public class GetUserClaimLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int Id { get; set; }

        public class GetUserClaimLookupByUserIdQueryHandler(IUserClaimRepository userClaimRepository, 
            IMediator mediator) : IRequestHandler<GetUserClaimLookupByUserIdQuery,
            IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserClaimRepository _userClaimRepository = userClaimRepository;
            private readonly IMediator _mediator = mediator;

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserClaimLookupByUserIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _userClaimRepository.GetUserClaimSelectedList(request.Id);
                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}