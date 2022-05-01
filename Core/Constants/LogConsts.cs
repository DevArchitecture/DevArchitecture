using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

namespace Core.Constants;
public class LogConsts
{
    public static Type FileLogger => typeof(FileLogger);
    public static Type LogstashLogger => typeof(LogstashLogger);
    public static Type MongoDbLogger => typeof(MongoDbLogger);
    public static Type MsSqlLogger => typeof(MsSqlLogger);
    public static Type MsTeamsLogger => typeof(MsTeamsLogger);
    public static Type OracleLogger => typeof(OracleLogger);
    public static Type PostgreSqlLogger => typeof(PostgreSqlLogger);
}
