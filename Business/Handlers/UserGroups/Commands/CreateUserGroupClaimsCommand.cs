using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Commands
{
    [SecuredOperation]
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

                return new SuccessResult(Messages.UserGroupsAdded);
            }

        }
    }
}
