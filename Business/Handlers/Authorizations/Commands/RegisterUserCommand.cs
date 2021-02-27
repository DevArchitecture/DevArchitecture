using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.Authorizations.ValidationRules;

namespace Business.Handlers.Authorizations.Commands
{
    
    public class RegisterUserCommand : IRequest<IResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }


        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;


            public RegisterUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }


            [SecuredOperation(Priority = 1)]
            [ValidationAspect(typeof(RegisterUserValidator), Priority = 2)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var userExits = await _userRepository.GetAsync(u => u.Email == request.Email);

                if (userExits != null)
                    return new ErrorResult(Messages.NameAlreadyExist);


                HashingHelper.CreatePasswordHash(request.Password, out var passwordSalt, out var passwordHash);
                var user = new User
                {
                    Email = request.Email,

                    FullName = request.FullName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}

