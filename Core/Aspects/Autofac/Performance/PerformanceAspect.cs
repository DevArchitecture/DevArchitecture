﻿using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    /// <summary>
    /// PerformanceAspect
    /// </summary>
    public class PerformanceAspect(int interval) : MethodInterception
    {
        private readonly int _interval = interval;
        private readonly Stopwatch _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine(
                    $"Performance: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }

            _stopwatch.Reset();
        }
    }
}