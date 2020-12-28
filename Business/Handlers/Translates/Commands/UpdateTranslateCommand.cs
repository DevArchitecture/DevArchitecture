
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Translates.ValidationRules;


namespace Business.Handlers.Translates.Commands
{
    /// <summary>
    /// CQRS yaklaşımında oluşturulmuş bir Command sınıfıdır. Bir kategorinin güncellenmesini sağlar
    /// </summary>
    [SecuredOperation]
    public class UpdateTranslateCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }

        public class UpdateTranslateCommandHandler : IRequestHandler<UpdateTranslateCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public UpdateTranslateCommandHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            /// <summary>
            /// Handler bir kategorinin var olup olmadığını doğrular
            /// eğer yoksa yeni bir kategorinin güncellenmesine izin verir.
            /// Aspectler her zaman hadler üzerinde kullanılmalıdır.
            /// Aşağıda validation, cacheremove ve log aspect Örnekleri kullanılmıştır.
            /// eğer kategori başarıyla eklenmişse sadece mesaj dÖner.
            /// Eğer farklı bir sınıf veya metod çağırılması gerekiyorsa MediatR kütüphanesi yardımıyla çağırılır
            /// Örneğin:  var result = await _mediator.Send(new GetAnimalsCountQuery());
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            [ValidationAspect(typeof(CreateTranslateValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateTranslateCommand request, CancellationToken cancellationToken)
            {
                var isThereTranslateRecord = await _translateRepository.GetAsync(u => u.Id == request.Id);

                isThereTranslateRecord.Id = request.Id;
                isThereTranslateRecord.LangId = request.LangId;
                isThereTranslateRecord.Value = request.Value;
                isThereTranslateRecord.Code = request.Code;


                _translateRepository.Update(isThereTranslateRecord);
                await _translateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

