using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Queries;

public class GetGroupLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public class
        GetGroupSelectListQueryHandler : IRequestHandler<GetGroupLookupQuery,
            IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMediator _mediator;
        public GetGroupSelectListQueryHandler(IGroupRepository groupRepository, IMediator mediator)
        {
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        [CacheAspect]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetGroupLookupQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _mediator.Send(new GetTenantQuery(), cancellationToken);
            if (tenant != null && tenant.Data.UserId == 1)
            {
                var groupLookups = await _groupRepository.GetListAsync();
                var groups = groupLookups.Select(x => new SelectionItem()
                {
                    Id = x.Id.ToString(),
                    Label = x.GroupName
                });
                return new SuccessDataResult<IEnumerable<SelectionItem>>(groups);
            }
            var list = await _groupRepository.GetListAsync(x => x.TenantId == tenant.Data.TenantId);
            var groupList = list.Select(x => new SelectionItem()
            {
                Id = x.Id.ToString(),
                Label = x.GroupName
            });
            return new SuccessDataResult<IEnumerable<SelectionItem>>(groupList);
        }
    }
}
