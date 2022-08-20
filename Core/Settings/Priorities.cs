namespace Core.Settings;

public class Priorities
{
    public int SecuredOperationAspectPriority { get; set; } = 0;
    public int ValidationAspectPriority { get; set; } = 1;
    public int PerformanceAspectPriority { get; set; } = 2;
    public int CacheAspectPriority { get; set; } = 3;
    public int CacheRemoveAspectPriority { get; set; } = 4;
    public int TransactionScopeAspectAsyncPriority { get; set; } = 5;
    public int LogAspectPriority { get; set; } = 6;
    public int ExceptionLogAspectPriority { get; set; } = 7;
}

