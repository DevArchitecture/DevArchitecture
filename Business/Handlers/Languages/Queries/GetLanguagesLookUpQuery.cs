using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Queries;

public class GetLanguagesLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public class
        GetLanguagesLookUpQueryHandler : IRequestHandler<GetLanguagesLookUpQuery,
            IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly ILanguageRepository _languageRepository;

        public GetLanguagesLookUpQueryHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [SecuredOperation]
        [PerformanceAspect]
        [CacheAspect]
        [LogAspect]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetLanguagesLookUpQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<SelectionItem>>(
                await _languageRepository.GetLanguagesLookUp());
        }
    }
}
