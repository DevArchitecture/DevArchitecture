using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    /// <summary>
    /// The Priority property can be used to determine the order in which Aspects will work on methods.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {
        }
    }
}