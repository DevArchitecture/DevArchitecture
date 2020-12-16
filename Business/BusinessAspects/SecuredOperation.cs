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
	/// Bu Aspect IHttpContextAccessor inject edilerek HttpContext'te bulunan kullanıcının rollerini kontrol eder 
	/// Handler üzerinde [SecuredOperation] şeklinde yazılarak kontrol edilir.
	/// Eğer aspecte geçerli bir yetki bulunamazsa exception fırlatır.
	/// 
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
