using System;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class OracleLogger : LoggerServiceBase
    {
        public OracleLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:OracleConfiguration")
                                .Get<OracleConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);
            // TODO: Not yet supported by netstandard 2.1

            // var seriLogConfig = new LoggerConfiguration()
            // .MinimumLevel.Verbose()
            // .WriteTo.Oracle(cfg =>
            // cfg.WithSettings(connectionString: logConfig.ConnectionString, "Logs")
            // .UseBurstBatch()
            // .CreateSink())
            // .CreateLogger();

            // Logger = seriLogConfig;
        }
    }
}