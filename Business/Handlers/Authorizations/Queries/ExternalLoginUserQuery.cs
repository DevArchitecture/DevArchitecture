using Business.Constants;
using Business.Services.Authentication;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using MediatR;
using Google.Apis.Auth;
using Core.Aspects.Autofac.Caching;
using Newtonsoft.Json;
using Core.Entities.Dtos;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Authorizations.ValidationRules;

namespace Business.Handlers.Authorizations.Queries;
public class ExternalLoginUserQuery : IRequest<IDataResult<AccessToken>>
{
    public string Provider { get; set; }
    public string Token { get; set; }

    public class ExternalLoginUserQueryHandler : IRequestHandler<ExternalLoginUserQuery, IDataResult<AccessToken>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICacheManager _cacheManager;

        public ExternalLoginUserQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper, ICacheManager cacheManager)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _cacheManager = cacheManager;
        }

        [ValidationAspect(typeof(ExternalLoginUserValidator), Priority = 1)]
        [LogAspect()]
        public async Task<IDataResult<AccessToken>> Handle(ExternalLoginUserQuery request, CancellationToken cancellationToken)
        {
            IDataResult<User> verifyResult;

            switch (request.Provider)
            {
                case "FACEBOOK":
                    verifyResult = await VerifyFacebookToken(request.Token);
                    break;
                case "GOOGLE":
                    verifyResult = await VerifyGoogleToken(request.Token);
                    break;
                default:
                    return new ErrorDataResult<AccessToken>(Messages.InvalidExternalAuthentication);
            }

            if (!verifyResult.Success)
            {
                return new ErrorDataResult<AccessToken>(verifyResult.Message);
            }

            var userData = verifyResult.Data;

            var user = await _userRepository.GetAsync(u => u.Email == userData.Email);

            if (user == null)
            {
                await RegisterUserAsync(userData);

                user = await _userRepository.GetAsync(u => u.Email == userData.Email);
            }

            var claims = _userRepository.GetClaims(user.UserId);

            var accessToken = _tokenHelper.CreateToken<DArchToken>(user);
            accessToken.Claims = claims.Select(x => x.Name).ToList();

            user.RefreshToken = accessToken.RefreshToken;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            _cacheManager.Add($"{CacheKeys.UserIdForClaim}={user.UserId}", claims.Select(x => x.Name));

            return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
        }

        [CacheRemoveAspect()]
        private async Task RegisterUserAsync(User user)
        {
            var registerUser = new User
            {
                TenantId = 1,
                CompanyId = 1,
                Email = user.Email,
                FullName = user.FullName,
                Status = true
            };

            _userRepository.Add(registerUser);
            await _userRepository.SaveChangesAsync();
        }

        private static async Task<IDataResult<User>> VerifyGoogleToken(string token)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(token);

                var user = new User
                {
                    TenantId = 1,
                    CompanyId = 1,
                    Email = payload.Email,
                    FullName = payload.Name
                };

                return new SuccessDataResult<User>(user);
            }
            catch
            {
                return new ErrorDataResult<User>(Messages.InvalidExternalAuthentication);
            }
        }

        private static async Task<IDataResult<User>> VerifyFacebookToken(string token)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri($"https://graph.facebook.com/")
            };

            var response = await client.GetAsync($"me?access_token={token}&fields=name,email");

            if (!response.IsSuccessStatusCode)
            {
                return new ErrorDataResult<User>(Messages.InvalidExternalAuthentication);
            }

            var content = await response.Content.ReadAsStringAsync();
            var payload = JsonConvert.DeserializeObject<FacebookUserDto>(content);

            var user = new User
            {
                TenantId=1,
                CompanyId=1,
                Email = payload.Email,
                FullName = payload.Name
            };

            return new SuccessDataResult<User>(user);
        }
    }
}
