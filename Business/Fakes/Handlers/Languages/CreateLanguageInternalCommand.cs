﻿using Business.Constants;
using Business.Handlers.Languages.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.Languages;

/// <summary>
///
/// </summary>
public class CreateLanguageInternalCommand : IRequest<IResult>
{
    public string Name { get; set; }
    public string Code { get; set; }


    public class CreateLanguageInternalCommandHandler : IRequestHandler<CreateLanguageInternalCommand, IResult>
    {
        private readonly ILanguageRepository _languageRepository;

        public CreateLanguageInternalCommandHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }


        [ValidationAspect(typeof(CreateLanguageValidator), Priority = 2)]
        [CacheRemoveAspect]
        public async Task<IResult> Handle(CreateLanguageInternalCommand request, CancellationToken cancellationToken)
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
