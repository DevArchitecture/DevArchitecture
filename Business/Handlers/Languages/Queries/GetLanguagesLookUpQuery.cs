
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Dtos;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Languages.Queries
{
    [SecuredOperation]
    public class GetLanguagesLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetLanguagesLookUpQueryHandler : IRequestHandler<GetLanguagesLookUpQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public GetLanguagesLookUpQueryHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetLanguagesLookUpQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(await _languageRepository.GetLanguagesLookUp());
            }
        }
    }
}