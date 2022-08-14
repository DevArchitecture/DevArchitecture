using Business.Constants;
using Business.Handlers.Authorizations.ValidationRules;
using Business.Services.Authentication;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Authorizations.Queries;

public class LoginUserQuery : IRequest<IDataResult<AccessToken>>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, IDataResult<AccessToken>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICacheManager _cacheManager;

        public LoginUserQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper, ICacheManager cacheManager)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _cacheManager = cacheManager;
        }

        [ValidationAspect(typeof(LoginUserValidator))]
        [LogAspect]
        public async Task<IDataResult<AccessToken>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Email == request.Email && u.Status);

            if (user == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                return new ErrorDataResult<AccessToken>(Messages.PasswordError);
            }

            return await Login(user);
        }

        internal async Task<IDataResult<AccessToken>> Login(User user)
        {
            var claims = _userRepository.GetClaims(user.UserId);

            var accessToken = _tokenHelper.CreateToken<DArchToken>(user);
            accessToken.Claims = claims.Select(x => x.Name).ToList();

            user.RefreshToken = accessToken.RefreshToken;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            _cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.UserId}", claims.Select(x => x.Name));

            return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
        }
    }
}
