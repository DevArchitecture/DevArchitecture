using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

public abstract class MethodInterception : MethodInterceptionBaseAttribute
{
    public override void Intercept(IInvocation invocation)
    {
        var isSuccess = true;
        OnBefore(invocation);
        try
        {
            invocation.Proceed();
            var result = invocation.ReturnValue as Task;
            result?.Wait();
        }
        catch (AggregateException e)
        {
            isSuccess = false;
            OnException(invocation, e);
            throw;
        }
        finally
        {
            if (isSuccess)
            {
                OnSuccess(invocation);
            }
        }

        OnAfter(invocation);
    }

    protected virtual void OnBefore(IInvocation invocation)
    {
    }

    protected virtual void OnAfter(IInvocation invocation)
    {
    }

    protected virtual void OnException(IInvocation invocation, AggregateException e)
    {
    }

    protected virtual void OnSuccess(IInvocation invocation)
    {
    }
}
