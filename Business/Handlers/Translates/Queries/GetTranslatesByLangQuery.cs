using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Queries
{
    public class GetTranslatesByLangQuery : IRequest<IDataResult<Dictionary<string, string>>>
    {
        public string Lang { get; set; }

        public class
            GetTranslatesByLangQueryHandler(ITranslateRepository translateRepository,
            IMediator mediator) : IRequestHandler<GetTranslatesByLangQuery,
                IDataResult<Dictionary<string, string>>>
        {
            private readonly ITranslateRepository _translateRepository = translateRepository;
            private readonly IMediator _mediator = mediator;

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Dictionary<string, string>>> Handle(GetTranslatesByLangQuery request, CancellationToken cancellationToken) => 
                new SuccessDataResult<Dictionary<string, string>>(
                    await _translateRepository.GetTranslatesByLang(request.Lang));
        }
    }
}