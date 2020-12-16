using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserGroups.Commands
{
    [SecuredOperation]
    public class UpdateUserGroupByGroupIdCommand: IRequest<IResult>
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int[] UserIds { get; set; }

        public class UpdateUserGroupByGroupIdCommandHandler : IRequestHandler<UpdateUserGroupByGroupIdCommand, IResult>
        {
            private IUserGroupRepository _userGroupRepository;
            public UpdateUserGroupByGroupIdCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            public async Task<IResult> Handle(UpdateUserGroupByGroupIdCommand request, CancellationToken cancellationToken)
            {
                var list = request.UserIds.Select(x => new UserGroup() { GroupId = request.GroupId, UserId = x });
                await _userGroupRepository.BulkInsertByGroupId(request.GroupId, list);
                await _userGroupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.UserGroupUpdated);
            }
        }
    }
}
