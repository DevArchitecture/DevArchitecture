using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Queries
{
    public class GetTranslateWordListQuery : IRequest<IDataResult<Dictionary<string, string>>>
    {
        public string Lang { get; set; }

        public class GetTranslateWordListQueryHandler : IRequestHandler<GetTranslateWordListQuery,
            IDataResult<Dictionary<string, string>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslateWordListQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Dictionary<string, string>>> Handle(GetTranslateWordListQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<Dictionary<string, string>>(
                    await _translateRepository.GetTranslateWordList(request.Lang));
            }
        }
    }
}