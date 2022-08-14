using Business.Constants;
using Business.Fakes.Handlers.Authorizations;
using Business.Handlers.Authorizations.ValidationRules;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Google.Apis.Auth;
using MediatR;
using Newtonsoft.Json;
using PasswordGenerator;
using static Business.Handlers.Authorizations.Queries.LoginUserQuery;

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
        private readonly IMediator _mediator;

        public ExternalLoginUserQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper, ICacheManager cacheManager, IMediator mediator)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _cacheManager = cacheManager;
            _mediator = mediator;
        }

        [ValidationAspect(typeof(ExternalLoginUserValidator))]
        [LogAspect]
        public async Task<IDataResult<AccessToken>> Handle(ExternalLoginUserQuery request, CancellationToken cancellationToken)
        {
            var externalUser = await VerifyToken(request);

            if (externalUser is null)
                return new ErrorDataResult<AccessToken>(Messages.InvalidExternalAuthentication);

            await _mediator.Send(new RegisterUserInternalCommand
            {
                Email = externalUser.Email,
                FullName = externalUser.FullName,
                Password = new Password(32).Next(),
            }, cancellationToken);

            var user = await _userRepository.GetAsync(u => u.Email == externalUser.Email);

            var loginUserQueryHandler = new LoginUserQueryHandler(_userRepository, _tokenHelper, _cacheManager);

            return await loginUserQueryHandler.Login(user);
        }

        private static async Task<User> VerifyToken(ExternalLoginUserQuery request)
        {
            return request.Provider switch
            {
                "FACEBOOK" => await VerifyFacebookToken(request.Token),
                "GOOGLE" => await VerifyGoogleToken(request.Token),
                _ => null,
            };
        }

        private static async Task<User> VerifyFacebookToken(string token)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri($"https://graph.facebook.com/")
            };

            var response = await client.GetAsync($"me?access_token={token}&fields=name,email");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var payload = JsonConvert.DeserializeObject<FacebookUserDto>(content);

            return new User
            {
                Email = payload.Email,
                FullName = payload.Name
            };
        }

        private static async Task<User> VerifyGoogleToken(string token)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(token);

                return new User
                {
                    Email = payload.Email,
                    FullName = payload.Name
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
