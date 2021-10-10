using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.UserGroups.Commands
{
    public class CreateUserGroupClaimsCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public IEnumerable<UserGroup> UserGroups { get; set; }

        public class CreateGroupClaimsCommandHandler : IRequestHandler<CreateUserGroupClaimsCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public CreateGroupClaimsCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateUserGroupClaimsCommand request, CancellationToken cancellationToken)
            {
                foreach (var claim in request.UserGroups)
                {
                    _userGroupRepository.Add(new UserGroup
                    {
                        GroupId = claim.GroupId,
                        UserId = request.UserId
                    });
                }

                await _userGroupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Added);
            }
        }
    }
}