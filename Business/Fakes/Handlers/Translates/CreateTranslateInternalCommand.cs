using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Business.Handlers.Translates.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.Translates
{
    /// <summary>
    ///
    /// </summary>
    public class CreateTranslateInternalCommand : IRequest<IResult>
    {
        public int LangId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }


        public class CreateTranslateInternalCommandHandler : IRequestHandler<CreateTranslateInternalCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public CreateTranslateInternalCommandHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }


            [ValidationAspect(typeof(CreateTranslateValidator), Priority = 2)]
            [CacheRemoveAspect()]
            public async Task<IResult> Handle(CreateTranslateInternalCommand request, CancellationToken cancellationToken)
            {
                var isThereTranslateRecord = _translateRepository.Query()
                    .Any(u => u.LangId == request.LangId && u.Code == request.Code);

                if (isThereTranslateRecord == true)
                {
                    return new ErrorResult(Messages.NameAlreadyExist);
                }

                var addedTranslate = new Translate
                {
                    LangId = request.LangId,
                    Value = request.Value,
                    Code = request.Code,
                };

                _translateRepository.Add(addedTranslate);
                await _translateRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}