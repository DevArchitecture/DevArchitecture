﻿namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
	using global::Serilog;
	using global::Serilog.Sinks.MSSqlServer;

	using System;
	using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
	using Core.Utilities.IoC;
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

			var seriLogConfig = new LoggerConfiguration()
										.WriteTo.MSSqlServer(connectionString: logConfig.ConnectionString, sinkOptions: sinkOpts)
										.CreateLogger();
			Logger = seriLogConfig;
		}
	}
}
