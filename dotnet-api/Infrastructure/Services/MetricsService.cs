using System.Diagnostics;
using System.Diagnostics.Metrics;
using Application.Interfaces;
public class MetricsService : IMetricsService
{
    private readonly Meter _meter;
    private readonly Counter<long> _requestCounter;
    private readonly Histogram<double> _latencyHistogram;

    public MetricsService()
    {
        _meter = new Meter("AppMetrics");
        _requestCounter = _meter.CreateCounter<long>(
            name: "app_requests_total",
            unit: "requests",
            description: "Total number of requests"
        );

        _latencyHistogram = _meter.CreateHistogram<double>(
            name: "app_request_duration_seconds",
            unit: "seconds",
            description: "Request duration in seconds"
        );
    }
    public void RecordRequest(string endpoint, int statusCode, double duration)
    {
        if (endpoint.ToLower() != "/metrics")
        {
            var tags = new TagList
            {
                { "endpoint", endpoint },
                { "status_code", statusCode.ToString() }
            };
            _requestCounter.Add(1, tags);
            _latencyHistogram.Record(duration, tags);
        }

    }
}