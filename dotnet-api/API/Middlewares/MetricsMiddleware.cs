using System.Diagnostics;
using Application.Interfaces;

public class MetricsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMetricsService _metricsService;
    public MetricsMiddleware(RequestDelegate next, IMetricsService metricsService)
    {
        _next = next;
        _metricsService = metricsService;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            sw.Stop();
            _metricsService.RecordRequest(
            context.Request.Path,
            context.Response.StatusCode,
            sw.Elapsed.TotalSeconds
            );
        }
    }
}
