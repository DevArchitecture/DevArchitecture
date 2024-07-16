using Autofac;
using Business.Constants;
using Business.DependencyResolvers;
using Business.Fakes.DArch;
using Business.Services.Authentication;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Core.Utilities.MessageBrokers;
using Core.Utilities.TaskScheduler;
using Core.Utilities.TaskScheduler.Hangfire;
using Core.Utilities.TaskScheduler.Hangfire.Models;
using Hangfire;
using Hangfire.InMemory;
using Hangfire.PostgreSql;
using Hangfire.RecurringJobExtensions;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;

namespace Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ClaimsPrincipal GetPrincipal(IServiceProvider sp) =>
                sp.GetService<IHttpContextAccessor>()?.HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));

            services.AddScoped<IPrincipal>(GetPrincipal);
            services.AddMemoryCache();

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule });

            services.AddTransient<IAuthenticationCoordinator, AuthenticationCoordinator>();

            services.AddSingleton<ConfigurationManager>();


            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, RMqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();

            var taskSchedulerConfig = Configuration.GetSection("TaskSchedulerOptions").Get<TaskSchedulerConfig>();

            if (taskSchedulerConfig.Enabled)
            {
                services.AddHangfire(config =>
                {
                    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
                    config.UseSimpleAssemblyNameTypeSerializer();
                    config.UseRecommendedSerializerSettings();
                    config.UseSerilogLogProvider();
                    config.UseDefaultActivator();
                    config.UseRecurringJob(typeof(BuildinRecurringJobs));
                    if (!string.IsNullOrWhiteSpace(
                            taskSchedulerConfig.RecurringJobsJsonFilePath))
                    {
                        config.UseRecurringJob(taskSchedulerConfig.RecurringJobsJsonFilePath);
                    }

                    switch (taskSchedulerConfig.StorageType)
                    {
                        case "postgresql":
                            var postgreSqlStorageOptions = new PostgreSqlStorageOptions
                            {
                                PrepareSchemaIfNecessary = true
                            };
                            config.UsePostgreSqlStorage(
                                configure => configure.UseNpgsqlConnection(taskSchedulerConfig.ConnectionString),
                                postgreSqlStorageOptions);
                            break;
                        case "mssql":
                            var sqlServerStorageOptions = new SqlServerStorageOptions
                            {
                                PrepareSchemaIfNecessary = true
                            };
                            config.UseSqlServerStorage(taskSchedulerConfig.ConnectionString,
                                sqlServerStorageOptions);
                            break;
                        case "inMemory":
                            var inMemoryOptions = new InMemoryStorageOptions
                            {
                                DisableJobSerialization = false
                            };
                            config.UseInMemoryStorage(inMemoryOptions);
                            break;
                    }
                });

                services.AddHangfireServer();
                services.AddTransient<IJobService, HangfireJobService>();
                services.AddTransient<IRecurringJobService, HangfireRecurringJobService>();
            }

            services.AddAutoMapper(typeof(ConfigurationManager));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<DisplayAttribute>()
                    ?.GetName();
            };
        }

        /// <summary>
        /// This method gets called by the Development
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();

            services.AddDbContext<ProjectDbContext, DArchInMemory>(ServiceLifetime.Transient);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Staging
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddDbContext<ProjectDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Production
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();


            services.AddDbContext<ProjectDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }
    }
}