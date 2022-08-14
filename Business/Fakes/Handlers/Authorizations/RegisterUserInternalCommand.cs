using AutoMapper;
using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using static Business.Handlers.Authorizations.Commands.RegisterUserCommand;

namespace Business.Fakes.Handlers.Authorizations;

public class RegisterUserInternalCommand : IRequest<IResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }


    public class RegisterUserInternalCommandHandler : IRequestHandler<RegisterUserInternalCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public RegisterUserInternalCommandHandler(IUserRepository userRepository, IMediator mediator, IMapper mapper)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(RegisterUserValidator))]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(RegisterUserInternalCommand request, CancellationToken cancellationToken)
        {
            var handler = new RegisterUserCommandHandler(_userRepository, _mediator);
            return await handler.Handle(_mapper.Map<RegisterUserCommand>(request), cancellationToken);
        }
    }
}
