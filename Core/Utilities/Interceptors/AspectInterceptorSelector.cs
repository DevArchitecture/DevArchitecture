using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using System.Reflection;

namespace Core.Utilities.Interceptors;

public class AspectInterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
        var methodAttributes = type.GetMethods()?.FirstOrDefault(x => x.Name == method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
        if (methodAttributes != null)
            classAttributes.AddRange(methodAttributes);

        classAttributes.Add(new ExceptionLogAspect());

        return classAttributes.OrderBy(x => x.Priority).ToArray();
    }
}
