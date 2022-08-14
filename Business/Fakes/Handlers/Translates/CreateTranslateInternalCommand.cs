using AutoMapper;
using Business.Handlers.Translates.Commands;
using Business.Handlers.Translates.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.Translates.Commands.CreateTranslateCommand;

namespace Business.Fakes.Handlers.Translates;

/// <summary>
///
/// </summary>
public class CreateTranslateInternalCommand : IRequest<IResult>
{
    public int LangId { get; set; }
    public string Value { get; set; }
    public string Code { get; set; }


    public class CreateTranslateInternalCommandHandler : IRequestHandler<CreateTranslateInternalCommand, IResult>
    {
        private readonly ITranslateRepository _translateRepository;
        private readonly IMapper _mapper;

        public CreateTranslateInternalCommandHandler(ITranslateRepository translateRepository, IMapper mapper)
        {
            _translateRepository = translateRepository;
            _mapper = mapper;
        }


        [ValidationAspect(typeof(CreateTranslateValidator))]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(CreateTranslateInternalCommand request, CancellationToken cancellationToken)
        {
            var handler = new CreateTranslateCommandHandler(_translateRepository);
            return await handler.Handle(_mapper.Map<CreateTranslateCommand>(request), cancellationToken);
        }
    }
}
