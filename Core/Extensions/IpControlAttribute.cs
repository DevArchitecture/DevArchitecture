using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Core.Extensions
{
    public class IpControlAttribute : ActionFilterAttribute
    {
        public IpControlAttribute()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            IPAddress remoteIp = context.HttpContext.Connection.RemoteIpAddress;

            var ips = SpecialConfigurationExtensions.GetWhiteList();

            if (!ips.Any(ip => IPAddress.Parse((string)ip).Equals(remoteIp)))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}