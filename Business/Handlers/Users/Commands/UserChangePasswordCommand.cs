using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Utilities.Security.Hashing;

namespace Business.Handlers.Users.Commands
{

    [SecuredOperation]
    public class UserChangePasswordCommand : IRequest<IResult>
    {

        public int UserId { get; set; }
        public string Password { get; set; }

        public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UserChangePasswordCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
            {

                var userExits = await _userRepository.GetAsync(u => u.UserId == request.UserId);
                if (userExits == null)
                    return new ErrorResult(Messages.UserNotFound);

                HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordSalt, out byte[] passwordHash);

                userExits.PasswordHash = passwordHash;
                userExits.PasswordSalt = passwordSalt;

                _userRepository.Update(userExits);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

