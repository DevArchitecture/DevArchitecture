using System;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MsTeamsLogger : LoggerServiceBase
    {
        public MsTeamsLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:MsTeamsConfiguration")
                                .Get<MsTeamsConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration()
                .WriteTo.MicrosoftTeams(logConfig.ChannelHookAddress)
                .CreateLogger();
        }
    }
}