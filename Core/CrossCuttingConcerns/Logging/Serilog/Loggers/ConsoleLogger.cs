using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers;

public class ConsoleLogger : LoggerServiceBase
{
    public ConsoleLogger()
    {
        var seriLogConfig = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        Logger = seriLogConfig;
    }
}