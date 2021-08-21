using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Queries
{
    public class GetLanguagesQuery : IRequest<IDataResult<IEnumerable<Language>>>
    {
        public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, IDataResult<IEnumerable<Language>>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public GetLanguagesQueryHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Language>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Language>>(await _languageRepository.GetListAsync());
            }
        }
    }
}