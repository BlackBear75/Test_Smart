using Microsoft.AspNetCore.Diagnostics;
using Test_Smart.Configuration;
using Test_Smart.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.ConfigureServices();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            await context.Response.WriteAsync(new
            {
                error = error.Error.Message,
                stackTrace = error.Error.StackTrace
            }.ToString());
        }
    });
});

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test_Smart API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseMiddleware<ApiKeyMiddleware>();

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();