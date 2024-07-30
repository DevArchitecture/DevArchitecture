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

namespace Business.Handlers.Translates.Commands
{
    public class DeleteTranslateCommand : IRequest<IResult>
    {
        public int Id { get; set; }


        public class DeleteTranslateCommandHandler(ITranslateRepository translateRepository, 
            IMediator mediator) : IRequestHandler<DeleteTranslateCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository = translateRepository;
            private readonly IMediator _mediator = mediator;

            [SecuredOperation(Priority = 1)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(DeleteTranslateCommand request, CancellationToken cancellationToken)
            {
                var translateToDelete = _translateRepository.Get(p => p.Id == request.Id);

                _translateRepository.Delete(translateToDelete);
                await _translateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}