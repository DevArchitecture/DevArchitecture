using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserGroups.Queries
{
    public class GetUserGroupLookupByUserIdQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int UserId { get; set; }

        public class GetUserGroupLookupByUserIdQueryHandler : IRequestHandler<GetUserGroupLookupByUserIdQuery,
            IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public GetUserGroupLookupByUserIdQueryHandler(IUserGroupRepository groupClaimRepository, IMediator mediator)
            {
                _groupClaimRepository = groupClaimRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupByUserIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _groupClaimRepository.GetUserGroupSelectedList(request.UserId);
                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}