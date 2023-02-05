using System.Linq;
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
    public class UpdateUserGroupByGroupIdCommand : IRequest<IResult>
    {       
        public int GroupId { get; set; }
        public int[] UserIds { get; set; }


        public class UpdateUserGroupByGroupIdCommandHandler : IRequestHandler<UpdateUserGroupByGroupIdCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;

            public UpdateUserGroupByGroupIdCommandHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateUserGroupByGroupIdCommand request, CancellationToken cancellationToken)
            {
                var list = request.UserIds.Select(x => new UserGroup() { GroupId = request.GroupId, UserId = x });
                await _userGroupRepository.BulkInsertByGroupId(request.GroupId, list);
                await _userGroupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}