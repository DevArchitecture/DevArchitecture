namespace Core.Utilities.TaskScheduler.Hangfire.Models;

public class TaskSchedulerConfig
{
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; }
    public string StorageType { get; set; }
    public string RecurringJobsJsonFilePath { get; set; }
    public string Path { get; set; }
    public string Title { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}