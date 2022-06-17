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

namespace Business.Handlers.Groups.Commands
{
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

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                var groupToUpdate = new Group
                {
                    Id = request.Id,
                    GroupName = request.GroupName
                };

                _groupRepository.Update(groupToUpdate);
                await _groupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}