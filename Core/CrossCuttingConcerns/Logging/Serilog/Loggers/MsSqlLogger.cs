namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    using System;
    using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
    using Core.Utilities.IoC;
    using global::Serilog;
    using global::Serilog.Sinks.MSSqlServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class MsSqlLogger : LoggerServiceBase
	{
		public MsSqlLogger()
		{
			var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

			var logConfig = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration")
					.Get<MsSqlConfiguration>() ?? throw new Exception(Utilities.Messages.SerilogMessages.NullOptionsMessage);
			var sinkOpts = new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true };

			var columnOpts = new ColumnOptions();
			columnOpts.Store.Remove(StandardColumn.Message);
			columnOpts.Store.Remove(StandardColumn.Properties);

			var seriLogConfig = new LoggerConfiguration()
					.WriteTo.MSSqlServer(connectionString: logConfig.ConnectionString, sinkOptions: sinkOpts, columnOptions: columnOpts)
					.CreateLogger();

			Logger = seriLogConfig;
		}
	}
}
