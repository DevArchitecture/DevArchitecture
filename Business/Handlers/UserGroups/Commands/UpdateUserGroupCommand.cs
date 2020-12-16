using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserGroups.Commands
{
    [SecuredOperation]
    public class UpdateUserGroupCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[] GroupId { get; set; }

        public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public UpdateUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            public async Task<IResult> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
            {

                var userGroupList = request.GroupId.Select(x => new UserGroup() { GroupId = x, UserId = request.UserId });

                await _userGroupRepository.BulkInsert(request.UserId, userGroupList);
                await _userGroupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.UserGroupUpdated);

            }
        }
    }
}
