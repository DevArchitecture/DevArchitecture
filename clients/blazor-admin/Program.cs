using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.Admin;
using Blazor.Admin.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();
builder.Services.AddScoped(_ => new HttpClient());
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<IAuthSessionStorage, AuthSessionStorage>();
builder.Services.AddScoped<ApiClient>();

await builder.Build().RunAsync();
