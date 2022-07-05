using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Languages.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Languages.Commands;

public class UpdateLanguageCommand : IRequest<IResult>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, IResult>
    {
        private readonly ILanguageRepository _languageRepository;

        public UpdateLanguageCommandHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [SecuredOperation(Priority = 1)]
        [ValidationAspect(typeof(UpdateLanguageValidator), Priority = 2)]
        [CacheRemoveAspect]
        [LogAspect]
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
