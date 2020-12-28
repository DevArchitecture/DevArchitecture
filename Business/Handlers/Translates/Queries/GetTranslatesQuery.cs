
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Translates.Queries
{
    [SecuredOperation]
    public class GetTranslatesQuery : IRequest<IDataResult<IEnumerable<Translate>>>
    {
        public class GetTranslatesQueryHandler : IRequestHandler<GetTranslatesQuery, IDataResult<IEnumerable<Translate>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslatesQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<Translate>>> Handle(GetTranslatesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Translate>>(await _translateRepository.GetListAsync());
            }
        }
    }
}