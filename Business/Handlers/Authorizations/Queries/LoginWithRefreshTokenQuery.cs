namespace Business.Handlers.Authorizations.Queries
{
	using Business.Constants;
	using Core.Aspects.Autofac.Logging;
	using Core.CrossCuttingConcerns.Caching;
	using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
	using Core.Entities.Concrete;
	using Core.Utilities.Results;
	using Core.Utilities.Security.Jwt;
	using DataAccess.Abstract;
	using MediatR;
	using System;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	public class LoginWithRefreshTokenQuery : IRequest<IResult>
	{
		public string RefreshToken { get; set; }

		public class LoginWithRefreshTokenQueryHandler : IRequestHandler<LoginWithRefreshTokenQuery, IResult>
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
				var claims = _userRepository.GetClaims(userToCheck.UserId);
				await _userRepository.SaveChangesAsync();
				_cacheManager.Remove($"{CacheKeys.UserIdForClaim}={userToCheck.UserId}");
				_cacheManager.Add($"{CacheKeys.UserIdForClaim}={userToCheck.UserId}", claims.Select(x => x.Name));


				var accessToken = _tokenHelper.CreateToken<AccessToken>(userToCheck);
				return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
			}
		}
	}
}
