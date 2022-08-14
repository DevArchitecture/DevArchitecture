using AutoMapper;
using Business.Handlers.Groups.Queries;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.Groups.Queries.GetGroupByNameQuery;

namespace Business.Fakes.Handlers.Groups;
public class GetGroupByNameInternalQuery : IRequest<IDataResult<Group>>
{
    public string GroupName { get; set; }

    public class GetGroupByNameInternalQueryHandler : IRequestHandler<GetGroupByNameInternalQuery, IDataResult<Group>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupByNameInternalQueryHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        [LogAspect]
        public async Task<IDataResult<Group>> Handle(GetGroupByNameInternalQuery request, CancellationToken cancellationToken)
        {
            var handler = new GetGroupByNameQueryHandler(_groupRepository);
            return await handler.Handle(_mapper.Map<GetGroupByNameQuery>(request), cancellationToken);
        }
    }
}
