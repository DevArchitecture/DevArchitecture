
using Business.BusinessAspects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserGroups.Queries
{
    [SecuredOperation]
    public class GetUserGroupQuery : IRequest<IDataResult<UserGroup>>
    {
        public int UserId { get; set; }

        public class GetUserGroupQueryHandler : IRequestHandler<GetUserGroupQuery, IDataResult<UserGroup>>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public GetUserGroupQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            public async Task<IDataResult<UserGroup>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
            {
                var userGroup = await _userGroupRepository.GetAsync(p => p.UserId == request.UserId);
                return new SuccessDataResult<UserGroup>(userGroup);
            }
        }
    }
}
