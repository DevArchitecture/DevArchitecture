using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MSTeamsLogger : LoggerServiceBase
    {
        public MSTeamsLogger()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:MSTeamsConfiguration")
                .Get<MSTeamsConfiguration>() ?? throw new Exception(Utilities.Messages.SerilogMessages.NullOptionsMessage);

            _logger = new LoggerConfiguration()
                    .WriteTo.MicrosoftTeams(logConfig.ChannelHookAdress)
                    .CreateLogger();
        }

    }
}
