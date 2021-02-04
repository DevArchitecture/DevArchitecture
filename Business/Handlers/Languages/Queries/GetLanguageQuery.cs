﻿
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;

namespace Business.Handlers.Languages.Queries
{
    [SecuredOperation]
    public class GetLanguageQuery : IRequest<IDataResult<Language>>
    {
        public int Id { get; set; }

        public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, IDataResult<Language>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public GetLanguageQueryHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Language>> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
            {
                var language = await _languageRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Language>(language);
            }
        }
    }
}
