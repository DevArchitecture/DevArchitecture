namespace Business.BusinessAspects
{
    public interface IActivityMonitor
    {
        void Save();
        ActivityMonitor.ActivitySummary Summarize();
        void Tick(string actionName, int ticks = 1, long msecs = 0, string userId = "");
    }
}