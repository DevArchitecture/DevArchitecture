using System;
using System.Linq.Expressions;

namespace Core.Utilities.TaskScheduler.Hangfire
{
    public interface IRecurringJobService
    {
        void AddOrUpdate(string jobId, Expression<Action> job, string cronExpression);

        void RemoveIfExists(string jobId);

        void Trigger(string jobId);
    }
}
