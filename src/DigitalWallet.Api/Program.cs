using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using DigitalWallet.Api.Health;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddHealthChecks()
    .AddCheck(
        name: "self",
        check: () => HealthCheckResult.Healthy(
            "Digital Wallet API is running."),
        tags: ["live", "ready"]);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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

app.Run();

public partial class Program;