using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Business
{
    public class ConfigurationManager
    {
        private readonly IConfiguration _configuration;
        public ApplicationMode Mode { get; private set; }

        public ConfigurationManager(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            Mode = (ApplicationMode)Enum.Parse(typeof(ApplicationMode), env.EnvironmentName);
        }
    }

    public enum ApplicationMode
    {
        Development,
        Profiling,
        Staging,
        Production,
    }

}
