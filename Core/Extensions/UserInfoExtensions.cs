using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class UserInfoExtensions
    {
        private static readonly IHttpContextAccessor HttpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        public static Guid GetUserId()
        {
            var result = HttpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier"))?.Value;
            return result != null ? new Guid(result) : Guid.Empty;
        }

    }
}
