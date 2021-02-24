
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Entities.Dtos;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Translates.Queries
{
    public class GetTranslateListDtoQuery : IRequest<IDataResult<IEnumerable<TranslateDto>>>
    {
        public class GetTranslateListDtoQueryHandler : IRequestHandler<GetTranslateListDtoQuery, IDataResult<IEnumerable<TranslateDto>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslateListDtoQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<TranslateDto>>> Handle(GetTranslateListDtoQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<TranslateDto>>(await _translateRepository.GetTranslateDto());
            }
        }
    }
}