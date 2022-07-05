using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Queries;

public class GetTranslatesQuery : IRequest<IDataResult<IEnumerable<Translate>>>
{
    public class
        GetTranslatesQueryHandler : IRequestHandler<GetTranslatesQuery, IDataResult<IEnumerable<Translate>>>
    {
        private readonly ITranslateRepository _translateRepository;

        public GetTranslatesQueryHandler(ITranslateRepository translateRepository)
        {
            _translateRepository = translateRepository;
        }

        [SecuredOperation(Priority = 1)]
        [PerformanceAspect]
        [CacheAspect(10)]
        [LogAspect]
        public async Task<IDataResult<IEnumerable<Translate>>> Handle(GetTranslatesQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<Translate>>(await _translateRepository.GetListAsync());
        }
    }
}
