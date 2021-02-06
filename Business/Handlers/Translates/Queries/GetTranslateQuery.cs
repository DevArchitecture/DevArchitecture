
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;

namespace Business.Handlers.Translates.Queries
{
    [SecuredOperation]
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
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<Translate>> Handle(GetTranslateQuery request, CancellationToken cancellationToken)
            {
                var translate = await _translateRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Translate>(translate);
            }
        }
    }
}
