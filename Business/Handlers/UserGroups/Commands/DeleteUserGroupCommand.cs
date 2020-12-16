using Business.BusinessAspects;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserGroups.Commands
{
    [SecuredOperation]
    public class DeleteUserGroupCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public DeleteUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            public async Task<IResult> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
            {
                var entityToDelete = await _userGroupRepository.GetAsync(x => x.UserId == request.Id);

                _userGroupRepository.Delete(entityToDelete);
                await _userGroupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.UserGroupDeleted);
            }
        }
    }
}
