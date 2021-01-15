using Autofac;
using AutoMapper;
using Business.BusinessAspects;
using Business.DependencyResolvers;
using Business.Services.Authentication;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.MongoDb;
using DataAccess.Concrete.MongoDb.Collections;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;


namespace Business
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BusinessStartup
	{
		protected readonly IHostEnvironment HostEnvironment;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="hostEnvironment"></param>
		public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
		{
			Configuration = configuration;
			HostEnvironment = hostEnvironment;
		}

		/// <summary>
		/// 
		/// </summary>
		public IConfiguration Configuration { get; }


		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container. 
		/// </summary>
		/// <remarks>
		/// Tüm konfigürasyonlar için ortaktır ve çağırılması gerekir. Aspnet core diğer
		/// metotlar olduğu için bu metodu çağırmaz.
		/// </remarks>
		/// <param name="services"></param>

		public virtual void ConfigureServices(IServiceCollection services)
		{


			Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>

							sp.GetService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity("Unknown"));

			services.AddScoped<IPrincipal>(getPrincipal);		


			services.AddDependencyResolvers(Configuration, new ICoreModule[]
			{
															new CoreModule()
			});

			services.AddTransient<IAuthenticationCoordinator, AuthenticationCoordinator>();

			services.AddSingleton<ConfigurationManager>();


			services.AddTransient<ITokenHelper, JwtHelper>();
			services.AddTransient<IElasticSearch, ElasticSearchManager>();

			services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
			services.AddTransient<IMessageConsumer, MqConsumerHelper>();

			services.AddAutoMapper(typeof(ConfigurationManager));
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

			ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
			{
				return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()?.GetName();
			};

		}

		/// <summary>
		/// Geliştirmede çağırılan konfigürasyondur.
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

			services.AddDbContext<ProjectDbContext, Fakes.SFw.SFwInMemory>(ServiceLifetime.Transient);
			services.AddSingleton<MongoDbContextBase, MongoDbContext>();



		}

		/// <summary>
		/// Geliştirmede çağırılan konfigürasyondur.
		/// </summary>
		/// <param name="services"></param> 
		public void ConfigureProfilingServices(IServiceCollection services)
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
		/// Sahnelemede çağırılan konfigürasyondur.
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
		/// Canlıda çağırılan konfigürasyondur.
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
