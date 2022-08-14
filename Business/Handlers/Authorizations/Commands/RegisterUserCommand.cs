using Business.Constants;
using Business.Fakes.Handlers.Groups;
using Business.Fakes.Handlers.UserGroups;
using Business.Handlers.Authorizations.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Authorizations.Commands;

public class RegisterUserCommand : IRequest<IResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }


    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;


        public RegisterUserCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(RegisterUserValidator))]
        [CacheRemoveAspect]
        [LogAspect]
        public async Task<IResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isThereAnyUser = await _userRepository.GetAsync(u => u.Email == request.Email);

            if (isThereAnyUser != null)
            {
                return new ErrorResult(Messages.NameAlreadyExist);
            }

            HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
            var user = new User
            {
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                TenantId = 1,
                CompanyId = 1
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return new SuccessResult(Messages.Added);
        }
    }
}
