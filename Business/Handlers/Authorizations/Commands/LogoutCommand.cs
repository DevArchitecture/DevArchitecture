using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Constants;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using IResult = Core.Utilities.Results.IResult;

namespace Business.Handlers.Authorizations.Commands
{
    /// <summary>
    /// Revokes the caller's access token and clears the stored refresh token.
    /// </summary>
    public class LogoutCommand : IRequest<IResult>
    {
        public class LogoutCommandHandler : IRequestHandler<LogoutCommand, IResult>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ICacheManager _cacheManager;
            private readonly IUserRepository _userRepository;
            private readonly TokenOptions _tokenOptions;

            public LogoutCommandHandler(
                IHttpContextAccessor httpContextAccessor,
                ICacheManager cacheManager,
                IUserRepository userRepository,
                IConfiguration configuration)
            {
                _httpContextAccessor = httpContextAccessor;
                _cacheManager = cacheManager;
                _userRepository = userRepository;
                _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            }

            public async Task<IResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
            {
                var principal = _httpContextAccessor.HttpContext?.User;
                var jti = principal?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                if (string.IsNullOrEmpty(jti))
                {
                    return new ErrorResult(Messages.AuthorizationsDenied);
                }

                // shortcut: revocation state lives in ICacheManager; the default in-memory cache forgets it on restart
                // and does not share it across instances, so use the Redis cache (CacheOptions) for multi-instance deployments.
                _cacheManager.Add($"{CacheKeys.RevokedTokenForJti}={jti}", true, _tokenOptions.AccessTokenExpiration + 1);

                var userId = principal.Claims
                    .FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
                if (int.TryParse(userId, out var id))
                {
                    var user = await _userRepository.GetAsync(u => u.UserId == id);
                    if (user != null)
                    {
                        user.RefreshToken = null;
                        _userRepository.Update(user);
                        await _userRepository.SaveChangesAsync();
                    }
                }

                return new SuccessResult(Messages.SuccessfulLogout);
            }
        }
    }
}
