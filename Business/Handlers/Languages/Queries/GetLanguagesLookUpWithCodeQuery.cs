using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Queries;

public class GetLanguagesLookUpWithCodeQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
{
    public class GetLanguagesLookUpQueryHandler : IRequestHandler<GetLanguagesLookUpWithCodeQuery,
        IDataResult<IEnumerable<SelectionItem>>>
    {
        private readonly ILanguageRepository _languageRepository;

        public GetLanguagesLookUpQueryHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetLanguagesLookUpWithCodeQuery request, CancellationToken cancellationToken)
        {
            return new SuccessDataResult<IEnumerable<SelectionItem>>(
                await _languageRepository.GetLanguagesLookUpWithCode());
        }
    }
}
