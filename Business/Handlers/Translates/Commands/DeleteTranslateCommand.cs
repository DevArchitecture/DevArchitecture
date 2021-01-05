
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Translates.Commands
{
    /// <summary>
    /// CQRS yaklaşımında oluşturulmuş bir Command sınıftır. Bir kategorinin silinmesini sağlar
    /// </summary>
    [SecuredOperation]
    public class DeleteTranslateCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteTranslateCommandHandler : IRequestHandler<DeleteTranslateCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public DeleteTranslateCommandHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }
            /// <summary>
            /// Aspectler her zaman handle üzerinde kullanılmalıdır.
            /// Kullanıcı arayüzünden bir Id alır ve silinmek istenen kategori doğrulanır.
            /// Kategori silinebilirse sadece mesaj döner.
            /// iş kuralları burada yazılır.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            [CacheRemoveAspect("Get")]
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

