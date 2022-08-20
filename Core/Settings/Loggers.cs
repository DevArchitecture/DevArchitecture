using Core.Constants;

namespace Core.Settings;

public class Loggers
{
    public Type ExceptionLogAspectLogger { get; set; } = LogConsts.FileLogger;
    public Type LogAspectLogger { get; set; } = LogConsts.FileLogger;
}

