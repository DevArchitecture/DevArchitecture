using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Queries;
public class GetGroupByNameQuery : IRequest<IDataResult<Group>>
{
    public string GroupName { get; set; }

    public class GetGroupByNameQueryHandler : IRequestHandler<GetGroupByNameQuery, IDataResult<Group>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupByNameQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [SecuredOperation]
        [LogAspect]
        public async Task<IDataResult<Group>> Handle(GetGroupByNameQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetAsync(x => x.GroupName == request.GroupName);

            return new SuccessDataResult<Group>(group);
        }
    }
}
