using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MongoDbLogger : LoggerServiceBase
    {
        public MongoDbLogger()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
            var logConfig = configuration.GetSection("SeriLogConfigurations:MongoDbConfiguration")
               .Get<MongoDbConfiguration>();

            _logger = new LoggerConfiguration()
                 .WriteTo.MongoDB(logConfig.ConnectionString, collectionName: logConfig.Collection)
                 .CreateLogger();
        }
    }
}
