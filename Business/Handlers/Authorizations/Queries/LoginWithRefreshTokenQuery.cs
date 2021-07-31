namespace Business.Handlers.Authorizations.Queries
{
    using Business.Constants;
    using Core.Aspects.Autofac.Logging;
    using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
    using Core.Entities.Concrete;
    using Core.Utilities.Results;
    using Core.Utilities.Security.Jwt;
    using DataAccess.Abstract;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginWithRefreshTokenQuery : IRequest<IResult>
    {
        public string RefreshToken { get; set; }

        public class LoginWithRefreshTokenQueryHandler : IRequestHandler<LoginWithRefreshTokenQuery, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;

            public LoginWithRefreshTokenQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(LoginWithRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var userToCheck = await _userRepository.GetByRefreshToken(request.RefreshToken);
                if (userToCheck == null)
                {
                    return new ErrorDataResult<User>(Messages.UserNotFound);
                }

                userToCheck.RefreshToken = Guid.NewGuid().ToString();
                _userRepository.Update(userToCheck);

                await _userRepository.SaveChangesAsync();

                var accessToken = _tokenHelper.CreateToken<AccessToken>(userToCheck);
                return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
            }
        }
    }
}
