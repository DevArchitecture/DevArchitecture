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

    public class ConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManager(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            Mode = (ApplicationMode)Enum.Parse(typeof(ApplicationMode), env.EnvironmentName);
        }

        public ApplicationMode Mode { get; private set; }
    }
}