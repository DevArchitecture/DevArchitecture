﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Queries;

public class GetLanguageQuery : IRequest<IDataResult<Language>>
{
    public int Id { get; set; }

    public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, IDataResult<Language>>
    {
        private readonly ILanguageRepository _languageRepository;

        public GetLanguageQueryHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [SecuredOperation(Priority = 1)]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<Language>> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetAsync(p => p.Id == request.Id);
            return new SuccessDataResult<Language>(language);
        }
    }
}
