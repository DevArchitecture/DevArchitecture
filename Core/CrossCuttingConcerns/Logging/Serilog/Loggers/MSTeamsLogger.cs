namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    using System;
    using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
    using Core.Utilities.IoC;
    using global::Serilog;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class MsTeamsLogger : LoggerServiceBase
    {
        public MsTeamsLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:MsTeamsConfiguration")
                .Get<MsTeamsConfiguration>() ?? throw new Exception(Utilities.Messages.SerilogMessages.NullOptionsMessage);

            Logger = new LoggerConfiguration()
                    .WriteTo.MicrosoftTeams(logConfig.ChannelHookAddress)
                    .CreateLogger();
        }

    }
}
