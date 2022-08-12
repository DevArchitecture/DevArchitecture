using Business.BusinessAspects;
using Business.Constants;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Commands;

public class CreateGroupCommand : IRequest<IResult>
{
    public string GroupName { get; set; }

    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, IResult>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMediator _mediator;

        public CreateGroupCommandHandler(IGroupRepository groupRepository, IMediator mediator)
        {
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        [SecuredOperation(Priority = 1)]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _mediator.Send(new GetTenantQuery(), cancellationToken);
            var group = new Group
            {
                TenantId = tenant.Data.TenantId,
                GroupName = request.GroupName
            };
            _groupRepository.Add(group);
            await _groupRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Added);
        }
    }
}
