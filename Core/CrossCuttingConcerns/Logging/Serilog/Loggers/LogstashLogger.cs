namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    using System;
    using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
    using Core.Utilities.IoC;
    using global::Serilog;
    using global::Serilog.Formatting.Elasticsearch;
    using global::Serilog.Sinks.Http.BatchFormatters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class LogstashLogger : LoggerServiceBase
    {
        public LogstashLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:LogstashConfiguration")
                .Get<LogstashConfiguration>() ?? throw new Exception(Utilities.Messages.SerilogMessages.NullOptionsMessage);

            var seriLogConfig = new LoggerConfiguration()
                    .WriteTo
                    .DurableHttpUsingFileSizeRolledBuffers(
                        requestUri: logConfig.Url,
                        batchFormatter: new ArrayBatchFormatter(),
                        textFormatter: new ElasticsearchJsonFormatter())
                    .CreateLogger();
            Logger = seriLogConfig;
        }
    }
}
