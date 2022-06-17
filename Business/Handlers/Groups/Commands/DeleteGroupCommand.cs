using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Groups.Commands
{
    public class DeleteGroupCommand : IRequest<IResult>
    {
        public int Id { get; set; }


        public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;

            public DeleteGroupCommandHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                var groupToDelete = await _groupRepository.GetAsync(x => x.Id == request.Id);

                _groupRepository.Delete(groupToDelete);
                await _groupRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}