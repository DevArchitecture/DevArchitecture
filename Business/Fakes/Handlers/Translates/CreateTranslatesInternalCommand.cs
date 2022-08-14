using Business.Constants;
using Business.Handlers.Translates.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Fakes.Handlers.Translates;
/// <summary>
///
/// </summary>
public class CreateTranslatesInternalCommand : IRequest<IResult>
{
    public Translate[] Translates { get; set; }


    public class CreateTranslatesInternalCommandHandler : IRequestHandler<CreateTranslatesInternalCommand, IResult>
    {
        private readonly ITranslateRepository _translateRepository;

        public CreateTranslatesInternalCommandHandler(ITranslateRepository translateRepository)
        {
            _translateRepository = translateRepository;
        }


        [ValidationAspect(typeof(CreateTranslateValidator))]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateTranslatesInternalCommand request, CancellationToken cancellationToken)
        {
            _translateRepository.AddRange(request.Translates);
            await _translateRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Added);
        }
    }
}
