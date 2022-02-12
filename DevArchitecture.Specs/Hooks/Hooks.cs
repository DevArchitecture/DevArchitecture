
using Autofac.Extensions.DependencyInjection;
using Core.CrossCuttingConcerns.Caching;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using WebAPI;

namespace DevArchitecture.Specs.Hooks
{
    [Binding, Scope(Feature = "Users")]
    public sealed class Hooks
    {
        private static IHost _host;

        [BeforeFeature]
        public static void BeforeFeature()
        {
            var rootPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("DevArchitecture.Specs"));

            var configuration = new ConfigurationBuilder()
              .SetBasePath(rootPath + "WebAPI")
              .AddEnvironmentVariables()
              .AddJsonFile("appsettings.json", optional: true)
              .AddJsonFile("appsettings.Development.json", optional: true)
              .Build();

            _host = new HostBuilder()
            .UseContentRoot(rootPath + "WebAPI")
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseEnvironment("Development")
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMemoryCache();
                services.AddSingleton<ICacheManager, Core.CrossCuttingConcerns.Caching.Microsoft.MemoryCacheManager>();
            })
            .ConfigureAppConfiguration(builder =>
            {
                builder.Sources.Clear();
                builder.AddConfiguration(configuration);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            }).Build();
            _host.StartAsync();
        }

        [AfterFeature]
        public static void AfterScenario()
        {
            _host.StopAsync();
        }
    }
}
