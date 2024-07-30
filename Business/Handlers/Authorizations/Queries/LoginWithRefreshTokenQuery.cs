using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Authorizations.Queries
{
    public class LoginWithRefreshTokenQuery : IRequest<IResult>
    {
        public string RefreshToken { get; set; }

        public class LoginWithRefreshTokenQueryHandler(IUserRepository userRepository,
            ITokenHelper tokenHelper, 
            ICacheManager cacheManager) : IRequestHandler<LoginWithRefreshTokenQuery, IResult>
        {
            private readonly IUserRepository _userRepository = userRepository;
            private readonly ITokenHelper _tokenHelper = tokenHelper;
            private readonly ICacheManager _cacheManager = cacheManager;

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(LoginWithRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var userToCheck = await _userRepository.GetByRefreshToken(request.RefreshToken);
                if (userToCheck == null)
                {
                    return new ErrorDataResult<User>(Messages.UserNotFound);
                }


				var claims = _userRepository.GetClaims(userToCheck.UserId);
				_cacheManager.Remove($"{CacheKeys.UserIdForClaim}={userToCheck.UserId}");
				_cacheManager.Add($"{CacheKeys.UserIdForClaim}={userToCheck.UserId}", claims.Select(x => x.Name));
				var accessToken = _tokenHelper.CreateToken<AccessToken>(userToCheck);
				userToCheck.RefreshToken = accessToken.RefreshToken;
				_userRepository.Update(userToCheck);
				await _userRepository.SaveChangesAsync();
				return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
			}
		}
	}
}

