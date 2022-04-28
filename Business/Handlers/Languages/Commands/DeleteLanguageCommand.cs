using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Commands;

public class DeleteLanguageCommand : IRequest<IResult>
{
    public int Id { get; set; }

    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, IResult>
    {
        private readonly ILanguageRepository _languageRepository;

        public DeleteLanguageCommandHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [CacheRemoveAspect()]
        [LogAspect(typeof(FileLogger))]
        [SecuredOperation(Priority = 1)]
        public async Task<IResult> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var languageToDelete = _languageRepository.Get(p => p.Id == request.Id);

            _languageRepository.Delete(languageToDelete);
            await _languageRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Deleted);
        }
    }
}
