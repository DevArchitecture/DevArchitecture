namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
	using System;
	using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
	using Core.Utilities.IoC;
	using global::Serilog;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	public class OracleLogger : LoggerServiceBase
	{
		public OracleLogger()
		{
			var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

			var logConfig = configuration.GetSection("SeriLogConfigurations:OracleConfiguration")
					.Get<OracleConfiguration>() ?? throw new Exception(Utilities.Messages.SerilogMessages.NullOptionsMessage);
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
