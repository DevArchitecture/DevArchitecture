using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

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

            if (!ips.Any(ip => IPAddress.Parse(ip).Equals(remoteIp)))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
