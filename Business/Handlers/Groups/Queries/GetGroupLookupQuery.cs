using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Queries
{
    public class GetGroupLookupQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetGroupSelectListQueryHandler : IRequestHandler<GetGroupLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IGroupRepository _groupRepository;

            public GetGroupSelectListQueryHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [CacheAspect(10)]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetGroupLookupQuery request, CancellationToken cancellationToken)
            {
                var list = await _groupRepository.GetListAsync();
                var groupList = list.Select(x => new SelectionItem()
                {
                    Id = x.Id.ToString(),
                    Label = x.GroupName
                });
                return new SuccessDataResult<IEnumerable<SelectionItem>>(groupList);
            }
        }
    }
}