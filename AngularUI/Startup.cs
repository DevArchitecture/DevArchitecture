using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AngularUI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			#region junkCode
			//services.AddControllersWithViews();
			// In production, the Angular files will be served from this directory
			//services.AddSpaStaticFiles(configuration =>
			//{
			//	configuration.RootPath = "ClientApp/dist";
			//});
			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			#region junkCode
			// To learn more about options for serving an Angular SPA from ASP.NET Core,
			// see https://go.microsoft.com/fwlink/?linkid=864501
			//if (env.IsDevelopment())
			//{
			//	app.UseDeveloperExceptionPage();
			//}
			//else
			//{				
			//	app.UseHsts();
			//}

			//app.UseHttpsRedirection();
			//app.UseStaticFiles();
			//if (!env.IsDevelopment())
			//{
			//	app.UseSpaStaticFiles();
			//}

			//app.UseRouting();

			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllerRoute(
			//						name: "default",
			//						pattern: "{controller}/{action=Index}/{id?}");
			//});
			#endregion

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "DevArchitectureUI";

				if (env.IsDevelopment())
				{
					spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}
