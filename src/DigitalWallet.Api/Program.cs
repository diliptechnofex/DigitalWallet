using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using DigitalWallet.Api.Health;
using DigitalWallet.Modules.Wallets.Infrastructure;
using DigitalWallet.Modules.Wallets.Presentation;
using DigitalWallet.Modules.Wallets.Presentation.Wallets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] =
            context.HttpContext.TraceIdentifier;
    };
});


builder.Services.AddWalletsInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHealthChecks()
    .AddCheck(
        name: "self",
        check: () => HealthCheckResult.Healthy(
            "Digital Wallet API is running."),
        tags: ["live", "ready"]);


//builder.Services.AddSwaggerServices();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwagger();
}
else
{
    app.UseExceptionHandler();
}


app.UseHttpsRedirection();

app.MapGet(
        "/",
        () =>
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version =
                assembly.GetName().Version?.ToString()
                ?? "unknown";

            return Results.Ok(
                new
                {
                    service = "digital-wallet-api",
                    version,
                    status = "running"
                });
        })
    .ExcludeFromDescription();

app.MapHealthChecks(
        "/health/live",
        new HealthCheckOptions
        {
            Predicate = registration =>
                registration.Tags.Contains("live"),

            ResponseWriter =
                HealthCheckResponseWriter.WriteAsync
        })
    .DisableHttpMetrics();

app.MapHealthChecks(
        "/health/ready",
        new HealthCheckOptions
        {
            Predicate = registration =>
                registration.Tags.Contains("ready"),

            ResponseWriter =
                HealthCheckResponseWriter.WriteAsync
        })
    .DisableHttpMetrics();
app.MapWalletsPresentation();
app.Run();


public partial class Program;