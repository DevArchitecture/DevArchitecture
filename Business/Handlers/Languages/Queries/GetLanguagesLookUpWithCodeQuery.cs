using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Queries
{
    public class GetLanguagesLookUpWithCodeQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetLanguagesLookUpQueryHandler(ILanguageRepository languageRepository,
            IMediator mediator) : IRequestHandler<GetLanguagesLookUpWithCodeQuery,
            IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly ILanguageRepository _languageRepository = languageRepository;
            private readonly IMediator _mediator = mediator;

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetLanguagesLookUpWithCodeQuery request, CancellationToken cancellationToken) =>
                new SuccessDataResult<IEnumerable<SelectionItem>>(
                    await _languageRepository.GetLanguagesLookUpWithCode());
        }
    }
}