using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Groups.Commands
{
    [SecuredOperation]
    public class UpdateGroupCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;

            public UpdateGroupCommandHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            public async Task<IResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                var groupToUpdate = new Group
                {
                    Id = request.Id,
                    GroupName = request.GroupName
                };

                _groupRepository.Update(groupToUpdate);
                await _groupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.GroupUpdated);
            }
        }
    }
}
