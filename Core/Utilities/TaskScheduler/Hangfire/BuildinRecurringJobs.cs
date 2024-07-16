using System;
using Hangfire;
using Hangfire.Server;
using Hangfire.RecurringJobExtensions;

namespace Core.Utilities.TaskScheduler.Hangfire;

public class BuildinRecurringJobs
{
    // [RecurringJob("*/1 * * * *")]
    // [Queue("jobs")]
    // public void TestJob1(PerformContext context)
    // {
    //     context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} TestJob1 Running ...");
    // }
    // [RecurringJob("*/2 * * * *", RecurringJobId = "TestJob2")]
    // [Queue("jobs")]
    // public void TestJob2(PerformContext context)
    // {
    //     context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} TestJob2 Running ...");
    // }
    // [RecurringJob("*/2 * * * *", "China Standard Time", "jobs")]
    // public void TestJob3(PerformContext context)
    // {
    //     context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} TestJob3 Running ...");
    // }
    // [RecurringJob("*/5 * * * *", "jobs")]
    // public void InstanceTestJob(PerformContext context)
    // {
    //     context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} InstanceTestJob Running ...");
    // }
    //
    // [RecurringJob("*/6 * * * *", "UTC", "jobs")]
    // public static void StaticTestJob(PerformContext context)
    // {
    //     context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} StaticTestJob Running ...");
    // }
}