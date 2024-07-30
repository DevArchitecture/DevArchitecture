using Hangfire;
using System;
using System.Linq.Expressions;

namespace Core.Utilities.TaskScheduler.Hangfire
{
    public class HangfireRecurringJobService(IRecurringJobManager backgroundJobClient) : IRecurringJobService
    {
        private readonly IRecurringJobManager _backgroundJobClient = backgroundJobClient;

        public void AddOrUpdate(string jobId, Expression<Action> job, string cronExpression) => 
            _backgroundJobClient?.AddOrUpdate(jobId, job, cronExpression);

        public void RemoveIfExists(string jobId) => _backgroundJobClient?.RemoveIfExists(jobId);

        public void Trigger(string jobId) => _backgroundJobClient?.Trigger(jobId);
    }
}