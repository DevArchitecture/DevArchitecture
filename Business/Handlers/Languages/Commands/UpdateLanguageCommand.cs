
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Languages.ValidationRules;


namespace Business.Handlers.Languages.Commands
{

    public class UpdateLanguageCommand : IRequest<IResult>
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, IResult>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMediator _mediator;

            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMediator mediator)
            {
                _languageRepository = languageRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(CreateLanguageValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
            {
                var isThereLanguageRecord = await _languageRepository.GetAsync(u => u.Id == request.Id);
               
                isThereLanguageRecord.Id = request.Id;
                isThereLanguageRecord.Name = request.Name;
                isThereLanguageRecord.Code = request.Code;


                _languageRepository.Update(isThereLanguageRecord);
                await _languageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

