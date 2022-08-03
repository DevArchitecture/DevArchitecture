using Business.BusinessAspects;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Queries;

public class GetGroupsQuery : IRequest<IDataResult<IEnumerable<Group>>>
{
    public int Id { get; set; }

    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IDataResult<IEnumerable<Group>>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMediator _mediator;
        public GetGroupsQueryHandler(IGroupRepository groupRepository, IMediator mediator)
        {
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        [SecuredOperation(Priority = 1)]
        [LogAspect]
        [CacheAspect]
        public async Task<IDataResult<IEnumerable<Group>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _mediator.Send(new GetTenantQuery());
            if (tenant != null && tenant.Data.UserId == 1)
            {
                return new SuccessDataResult<IEnumerable<Group>>(await _groupRepository.GetListAsync());
            }
            return new SuccessDataResult<IEnumerable<Group>>(await _groupRepository.GetListAsync(x => x.TenantId == tenant.Data.TenantId));
        }
    }
}
