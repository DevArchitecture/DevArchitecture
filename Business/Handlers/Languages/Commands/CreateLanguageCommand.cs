
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Languages.ValidationRules;
using Core.Entities.Concrete;

namespace Business.Handlers.Languages.Commands
{
    /// <summary>
    ///
    /// </summary>

    public class CreateLanguageCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public string Code { get; set; }


        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, IResult>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;
            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(CreateLanguageValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            {
                var isThereLanguageRecord = _languageRepository.Query().Any(u => u.Name == request.Name);

                if (isThereLanguageRecord)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedLanguage = new Language
                {
                    Name = request.Name,
                    Code = request.Code,

                };

                _languageRepository.Add(addedLanguage);
                await _languageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}