using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Http.BatchFormatters;
using System;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class LogstashLogger : LoggerServiceBase
    {
        public LogstashLogger()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:LogstashConfiguration")
                .Get<LogstashConfiguration>() ?? throw new Exception(Utilities.Messages.SerilogMessages.NullOptionsMessage);

            var seriLogConfig = new LoggerConfiguration()
                    .WriteTo
                    .DurableHttpUsingFileSizeRolledBuffers(
                        requestUri: logConfig.URL,
                        batchFormatter: new ArrayBatchFormatter(),
                        textFormatter: new ElasticsearchJsonFormatter()
                     )
                    .CreateLogger();
            _logger = seriLogConfig;
        }
    }
}
