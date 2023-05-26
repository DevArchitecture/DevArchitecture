using System;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;

namespace Core.Aspects.Autofac.Transaction
{
    /// <summary>
    /// TransactionScopeAspect
    /// </summary>
    public class TransactionScopeAspectAsync : MethodInterception
    {

        public static void InterceptDbContext(IInvocation invocation)
        {
            var db = ServiceTool.ServiceProvider.GetService(default) as DbContext;
            using var transactionScope = db.Database.BeginTransaction();
            try
            {
                invocation.Proceed();
                transactionScope.Commit();
            }
            finally
            {
                transactionScope.Rollback();
            }
        }

        public override void Intercept(IInvocation invocation)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                invocation.Proceed();
                transactionScope.Complete();
            }
            catch (System.Exception ex)
            {
                transactionScope.Rollback();
                throw;
            }
        }
    }
}