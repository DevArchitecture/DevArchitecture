using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class SpecialConfigurationExtensions
    {
        private static readonly IConfiguration Configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        public static List<string> GetWhiteList()
        {
            return Configuration.GetSection("WhiteList").AsEnumerable().Where(ip => !string.IsNullOrEmpty(ip.Value)).Select(ip => ip.Value).ToList();
        }
        public static IConfigurationSection GetName(string name)
        {
            return Configuration.GetSection(name);
        }

    }
}
