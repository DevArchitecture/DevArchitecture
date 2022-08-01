using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Authorizations.Queries;

public class LoginWithRefreshTokenQuery : IRequest<IDataResult<AccessToken>>
{
    public string RefreshToken { get; set; }

    public class LoginWithRefreshTokenQueryHandler : IRequestHandler<LoginWithRefreshTokenQuery, IDataResult<AccessToken>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICacheManager _cacheManager;

        public LoginWithRefreshTokenQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper, ICacheManager cacheManager)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _cacheManager = cacheManager;
        }

        [LogAspect]
        public async Task<IDataResult<AccessToken>> Handle(LoginWithRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var userToCheck = await _userRepository.GetByRefreshToken(request.RefreshToken);
            if (userToCheck == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
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

