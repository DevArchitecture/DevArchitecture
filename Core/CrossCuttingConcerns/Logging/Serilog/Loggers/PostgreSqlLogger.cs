using System;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class PostgreSqlLogger : LoggerServiceBase
    {
        public PostgreSqlLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:PostgreConfiguration")
                                .Get<PostgreConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);

            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                { "MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                { "Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                { "TimeStamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                { "Exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
            };


            var seriLogConfig = new LoggerConfiguration()
                .WriteTo.PostgreSQL(connectionString: logConfig.ConnectionString, tableName: "Logs", columnWriters, needAutoCreateTable: false)
                .CreateLogger();
            Logger = seriLogConfig;
        }
    }
}