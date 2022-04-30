using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Languages.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Commands;

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

        public CreateLanguageCommandHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [SecuredOperation(Priority = 1)]
        [ValidationAspect(typeof(CreateLanguageValidator), Priority = 2)]
        [CacheRemoveAspect()]
        [LogAspect()]
        public async Task<IResult> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var isThereLanguageRecord = _languageRepository.Query().Any(u => u.Name == request.Name);

            if (isThereLanguageRecord)
            {
                return new ErrorResult(Messages.NameAlreadyExist);
            }

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
