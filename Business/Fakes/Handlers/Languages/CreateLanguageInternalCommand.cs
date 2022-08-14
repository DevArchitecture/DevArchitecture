using AutoMapper;
using Business.Handlers.Languages.Commands;
using Business.Handlers.Languages.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.Languages.Commands.CreateLanguageCommand;

namespace Business.Fakes.Handlers.Languages;

/// <summary>
///
/// </summary>
public class CreateLanguageInternalCommand : IRequest<IResult>
{
    public string Name { get; set; }
    public string Code { get; set; }


    public class CreateLanguageInternalCommandHandler : IRequestHandler<CreateLanguageInternalCommand, IResult>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public CreateLanguageInternalCommandHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }


        [ValidationAspect(typeof(CreateLanguageValidator))]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateLanguageInternalCommand request, CancellationToken cancellationToken)
        {
            var handler = new CreateLanguageCommandHandler(_languageRepository);
            return await handler.Handle(_mapper.Map<CreateLanguageCommand>(request), cancellationToken);
        }
    }
}
