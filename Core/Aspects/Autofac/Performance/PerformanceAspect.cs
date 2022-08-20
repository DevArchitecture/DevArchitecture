﻿using Castle.DynamicProxy;
using Core.Settings;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance;

/// <summary>
/// PerformanceAspect
/// </summary>
public class PerformanceAspect : MethodInterception
{
    private readonly int _interval;
    private readonly Stopwatch _stopwatch;

    public PerformanceAspect() : this(DevArchitectureSettings.Intervals.PerformanceAspectInterval)
    {
        Priority = DevArchitectureSettings.Priorities.PerformanceAspectPriority;
    }
    public PerformanceAspect(int interval)
    {
        _interval = interval;
        _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
    }

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
