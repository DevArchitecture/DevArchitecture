using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Translates.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Translates.Commands;

public class UpdateTranslateCommand : IRequest<IResult>
{
    public int Id { get; set; }
    public int LangId { get; set; }
    public string Value { get; set; }
    public string Code { get; set; }

    public class UpdateTranslateCommandHandler : IRequestHandler<UpdateTranslateCommand, IResult>
    {
        private readonly ITranslateRepository _translateRepository;

        public UpdateTranslateCommandHandler(ITranslateRepository translateRepository)
        {
            _translateRepository = translateRepository;
        }

        [SecuredOperation]
        [ValidationAspect(typeof(CreateTranslateValidator))]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(UpdateTranslateCommand request, CancellationToken cancellationToken)
        {
            var isThereTranslateRecord = await _translateRepository.GetAsync(u => u.Id == request.Id);

            isThereTranslateRecord.Id = request.Id;
            isThereTranslateRecord.LangId = request.LangId;
            isThereTranslateRecord.Value = request.Value;
            isThereTranslateRecord.Code = request.Code;


            _translateRepository.Update(isThereTranslateRecord);
            await _translateRepository.SaveChangesAsync();
            return new SuccessResult(Messages.Updated);
        }
    }
}
