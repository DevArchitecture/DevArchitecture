using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Translates.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Commands
{
    /// <summary>
    ///
    /// </summary>
    public class CreateTranslateCommand : IRequest<IResult>
    {
        public int LangId { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }


        public class CreateTranslateCommandHandler : IRequestHandler<CreateTranslateCommand, IResult>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public CreateTranslateCommandHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(CreateTranslateValidator), Priority = 2)]
            [CacheRemoveAspect()]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateTranslateCommand request, CancellationToken cancellationToken)
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