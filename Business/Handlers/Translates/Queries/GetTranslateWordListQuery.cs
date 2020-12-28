
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
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Translates.Queries
{
    [SecuredOperation]
    public class GetTranslateWordListQuery : IRequest<IDataResult<Dictionary<string,string>>>
    {
        public string Lang { get; set; }
        public class GetTranslateWordListQueryHandler : IRequestHandler<GetTranslateWordListQuery, IDataResult<Dictionary<string, string>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslateWordListQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Dictionary<string, string>>> Handle(GetTranslateWordListQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<Dictionary<string, string>>(await _translateRepository.GetTranslateWordList(request.Lang));
            }
        }
    }
}