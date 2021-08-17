using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Queries
{
    public class GetTranslateQuery : IRequest<IDataResult<Translate>>
    {
        public int Id { get; set; }

        public class GetTranslateQueryHandler : IRequestHandler<GetTranslateQuery, IDataResult<Translate>>
        {
            private readonly ITranslateRepository _translateRepository;
            private readonly IMediator _mediator;

            public GetTranslateQueryHandler(ITranslateRepository translateRepository, IMediator mediator)
            {
                _translateRepository = translateRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Translate>> Handle(GetTranslateQuery request, CancellationToken cancellationToken)
            {
                var translate = await _translateRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Translate>(translate);
            }
        }
    }
}