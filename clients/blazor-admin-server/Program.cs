using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Serves Blazor WebAssembly _framework assets from the referenced client project (fixes DevServer wasm 404).
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapGet("/health", () => Results.Ok("ok"));

app.MapFallbackToFile("index.html");

app.Run();
