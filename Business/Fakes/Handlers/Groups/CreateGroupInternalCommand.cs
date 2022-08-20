using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.Groups;
public class CreateGroupInternalCommand : IRequest<IResult>
{
    public int TenantId { get; set; }
    public string GroupName { get; set; }

    public class CreateGroupInternalCommandHandler : IRequestHandler<CreateGroupInternalCommand, IResult>
    {
        private readonly IGroupRepository _groupRepository;

        public CreateGroupInternalCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateGroupInternalCommand request, CancellationToken cancellationToken)
        {
            var isThereAnyGroupRecord = _groupRepository.Query().Any(x => x.GroupName == request.GroupName);

            if (isThereAnyGroupRecord)
                return new ErrorResult(Messages.NameAlreadyExist);

            var group = new Group
            {
                TenantId = request.TenantId,
                GroupName = request.GroupName
            };
            _groupRepository.Add(group);
            await _groupRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Added);
        }
    }
}