using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Business
{
    public enum ApplicationMode
    {
        Development,
        Profiling,
        Staging,
        Production,
    }

    public class ConfigurationManager(IConfiguration configuration, IHostEnvironment env)
    {
        private readonly IConfiguration _configuration = configuration;

        public ApplicationMode Mode { get; private set; } = (ApplicationMode)Enum.Parse(typeof(ApplicationMode), env.EnvironmentName);
    }
}