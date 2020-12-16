
using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    /// <summary>
    /// Priority özelliği Aspectlerin metodlar üzerinde çalışması sırasını belirlemek için kullanılabilir.
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
