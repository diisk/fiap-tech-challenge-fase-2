namespace Application.Interfaces
{
    public interface IMetricsService
    {
        void RecordRequest(string endpoint, int statusCode, double duration);

    }
}
