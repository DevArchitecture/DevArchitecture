using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Commands;

public class DeleteTranslateCommand : IRequest<IResult>
{
    public int Id { get; set; }

    public class DeleteTranslateCommandHandler : IRequestHandler<DeleteTranslateCommand, IResult>
    {
        private readonly ITranslateRepository _translateRepository;

        public DeleteTranslateCommandHandler(ITranslateRepository translateRepository)
        {
            _translateRepository = translateRepository;
        }

        [SecuredOperation(Priority = 1)]
        [CacheRemoveAspect()]
        [LogAspect()]
        public async Task<IResult> Handle(DeleteTranslateCommand request, CancellationToken cancellationToken)
        {
            var translateToDelete = _translateRepository.Get(p => p.Id == request.Id);

            _translateRepository.Delete(translateToDelete);
            await _translateRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Deleted);
        }
    }
}
