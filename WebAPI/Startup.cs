using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using System.Text.Json;
using Business;
using Business.Helpers;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.Jwt;
using Core.Utilities.TaskScheduler.Hangfire.Models;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using ConfigurationManager = Business.ConfigurationManager;

namespace WebAPI
{
    /// <summary>
    ///
    /// </summary>
    public partial class Startup : BusinessStartup
    {
        /// <summary>
        /// Constructor of <c>Startup</c>
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostEnvironment"></param>
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
            : base(configuration, hostEnvironment)
        {
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            // Business katmanında olan dependency tanımlarının bir metot üzerinden buraya implemente edilmesi.

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddApiVersioning(v =>
            {
                v.DefaultApiVersion = new ApiVersion(1, 0);
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.ReportApiVersions = true;
                v.ApiVersionReader = new HeaderApiVersionReader("x-dev-arch-version");
            });

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DevArchitecture API",
                    Version = "v1",
                    Description = "Layered .NET architecture with CQRS pattern. Supports multiple admin clients (Vue, Angular, React, Blazor).",
                    Contact = new OpenApiContact
                    {
                        Name = "DevArchitecture",
                        Url = new Uri("https://github.com/DevArchitecture/DevArchitecture")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                if (File.Exists(xmlFile))
                {
                    c.IncludeXmlComments(xmlFile);
                }
            });

            services.AddTransient<FileLogger>();
            services.AddTransient<PostgreSqlLogger>();
            services.AddTransient<MsSqlLogger>();
            services.AddScoped<IpControlAttribute>();

            var healthChecksBuilder = services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());

            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(sqlConnectionString))
            {
                healthChecksBuilder.AddSqlServer(
                    sqlConnectionString,
                    name: "sqlserver",
                    timeout: TimeSpan.FromSeconds(5),
                    tags: new[] { "db", "sql", "ready" });
            }

            var schedulerConfig = Configuration.GetSection("TaskSchedulerOptions").Get<TaskSchedulerConfig>();
            if (schedulerConfig?.Enabled == true)
            {
                healthChecksBuilder.AddHangfire(
                    options => options.MaximumJobsFailed = 5,
                    name: "hangfire",
                    timeout: TimeSpan.FromSeconds(5),
                    tags: new[] { "scheduler", "hangfire", "ready" });
            }

            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024 * 1024; // 1 MB
                options.SizeLimit = 100 * 1024 * 1024; // 100 MB
            });

            var rateLimitingConfig = Configuration.GetSection("RateLimiting");

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.OnRejected = async (context, cancellationToken) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    var retryAfter = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfterTime)
                        ? (int)retryAfterTime.TotalSeconds
                        : 60;
                    var response = new
                    {
                        statusCode = 429,
                        message = "Too many requests. Please try again later.",
                        retryAfter
                    };
                    await context.HttpContext.Response.WriteAsync(
                        JsonSerializer.Serialize(response), cancellationToken);
                };

                var authConfig = rateLimitingConfig.GetSection("Auth");
                options.AddFixedWindowLimiter("auth", limiterOptions =>
                {
                    limiterOptions.PermitLimit = authConfig.GetValue<int>("PermitLimit");
                    limiterOptions.Window = TimeSpan.FromMinutes(authConfig.GetValue<int>("WindowMinutes"));
                    limiterOptions.QueueLimit = authConfig.GetValue<int>("QueueLimit");
                });

                var crudConfig = rateLimitingConfig.GetSection("Crud");
                options.AddFixedWindowLimiter("crud", limiterOptions =>
                {
                    limiterOptions.PermitLimit = crudConfig.GetValue<int>("PermitLimit");
                    limiterOptions.Window = TimeSpan.FromMinutes(crudConfig.GetValue<int>("WindowMinutes"));
                    limiterOptions.QueueLimit = crudConfig.GetValue<int>("QueueLimit");
                });

                var readConfig = rateLimitingConfig.GetSection("Read");
                options.AddFixedWindowLimiter("read", limiterOptions =>
                {
                    limiterOptions.PermitLimit = readConfig.GetValue<int>("PermitLimit");
                    limiterOptions.Window = TimeSpan.FromMinutes(readConfig.GetValue<int>("WindowMinutes"));
                    limiterOptions.QueueLimit = readConfig.GetValue<int>("QueueLimit");
                });
            });

            base.ConfigureServices(services);
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // VERY IMPORTANT. Since we removed the build from AddDependencyResolvers, let's set the Service provider manually.
            // By the way, we can construct with DI by taking type to avoid calling static methods in aspects.
            ServiceTool.ServiceProvider = app.ApplicationServices;


            var configurationManager = app.ApplicationServices.GetService<ConfigurationManager>();
            switch (configurationManager.Mode)
            {
                case ApplicationMode.Development:
                    _ = app.UseDbFakeDataCreator();
                    break;

                case ApplicationMode.Profiling:
                case ApplicationMode.Staging:

                    break;
                case ApplicationMode.Production:
                    break;
            }

            app.UseDeveloperExceptionPage();

            app.UseDbOperationClaimCreator().GetAwaiter().GetResult();
            
            if (!env.IsProduction())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "DevArchitecture");
                    c.DocExpansion(DocExpansion.None);
                });
            }
            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRateLimiter();

            app.UseAuthentication();

            app.UseAuthorization();

            // Make Turkish your default language. It shouldn't change according to the server.
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
            });

            var cultureInfo = new CultureInfo("tr-TR");
            cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseStaticFiles();

            var taskSchedulerConfig = Configuration.GetSection("TaskSchedulerOptions").Get<TaskSchedulerConfig>();

            if (taskSchedulerConfig?.Enabled == true)
            {
                app.UseHangfireDashboard(taskSchedulerConfig.Path, new DashboardOptions
                {
                    DashboardTitle = taskSchedulerConfig.Title,
                    Authorization = new[]
                    {
                        new HangfireCustomBasicAuthenticationFilter
                        {
                            User = taskSchedulerConfig.Username,
                            Pass = taskSchedulerConfig.Password
                        }
                    }
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        context.Response.ContentType = "application/json";
                        var response = new
                        {
                            status = report.Status.ToString(),
                            checks = report.Entries.Select(e => new
                            {
                                name = e.Key,
                                status = e.Value.Status.ToString(),
                                duration = e.Value.Duration.TotalMilliseconds,
                                description = e.Value.Description,
                                exception = e.Value.Exception?.Message
                            }),
                            totalDuration = report.TotalDuration.TotalMilliseconds
                        };
                        await context.Response.WriteAsJsonAsync(response);
                    }
                });
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => false
                });
            });
        }
    }
}