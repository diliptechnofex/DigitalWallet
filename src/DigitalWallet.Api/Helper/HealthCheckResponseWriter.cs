using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DigitalWallet.Api.Health;

internal static class HealthCheckResponseWriter
{
    private static readonly JsonSerializerOptions SerializerOptions =
        new(JsonSerializerDefaults.Web)
        {
            WriteIndented = false
        };

    internal static Task WriteAsync(
        HttpContext context,
        HealthReport report)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(report);

        context.Response.ContentType =
            "application/json; charset=utf-8";

        var response = new HealthCheckResponse(
            Status: report.Status.ToString(),
            TotalDurationMilliseconds:
                report.TotalDuration.TotalMilliseconds,
            Checks: report.Entries.Select(
                entry => new HealthCheckItem(
                    Name: entry.Key,
                    Status: entry.Value.Status.ToString(),
                    Description: entry.Value.Description,
                    DurationMilliseconds:
                        entry.Value.Duration.TotalMilliseconds)));

        var json = JsonSerializer.Serialize(
            response,
            SerializerOptions);

        return context.Response.WriteAsync(
            json,
            context.RequestAborted);
    }

    private sealed record HealthCheckResponse(
        string Status,
        double TotalDurationMilliseconds,
        IEnumerable<HealthCheckItem> Checks);

    private sealed record HealthCheckItem(
        string Name,
        string Status,
        string? Description,
        double DurationMilliseconds);
}