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
            GetTranslatesByLangQueryHandler : IRequestHandler<GetTranslatesByLangQuery,
                IDataResult<Dictionary<string, string>>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslatesByLangQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }


            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Dictionary<string, string>>> Handle(GetTranslatesByLangQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<Dictionary<string, string>>(
                    await _translateRepository.GetTranslatesByLang(request.Lang));
            }
        }
    }
}