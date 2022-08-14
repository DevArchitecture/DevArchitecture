using AutoMapper;
using Business.Handlers.UserGroups.Commands;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.UserGroups.Commands.CreateUserGroupCommand;

namespace Business.Fakes.Handlers.UserGroups;
public class CreateUserGroupInternalCommand : IRequest<IResult>
{
    public int GroupId { get; set; }
    public int UserId { get; set; }

    public class CreateUserGroupInternalCommandHandler : IRequestHandler<CreateUserGroupInternalCommand, IResult>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;

        public CreateUserGroupInternalCommandHandler(IUserGroupRepository userGroupRepository, IMapper mapper)
        {
            _userGroupRepository = userGroupRepository;
            _mapper = mapper;
        }

        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateUserGroupInternalCommand request, CancellationToken cancellationToken)
        {
            var handler = new CreateUserGroupCommandHandler(_userGroupRepository);
            return await handler.Handle(_mapper.Map<CreateUserGroupCommand>(request), cancellationToken);
        }
    }
}
