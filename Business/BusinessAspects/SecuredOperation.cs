using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security;

namespace Business.BusinessAspects
{
	/// <summary>
	///This Aspect control the user's roles in HttpContext by inject the IHttpContextAccessor.
	///It is checked by writing as [SecuredOperation] on the handler.
	///If a valid authorization cannot be found in aspec, it throws an exception.
	/// </summary>

	public class SecuredOperation : MethodInterception
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SecuredOperation()
		{
			_httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
		}

		protected override void OnBefore(IInvocation invocation)
		{
			var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
			var operationName = invocation.TargetType.ReflectedType.Name;
			if (roleClaims.Contains(operationName))
				return;

			throw new SecurityException(Messages.AuthorizationsDenied);
		}
	}
}
