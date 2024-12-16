namespace Application.Interfaces
{
    public interface ICacheService
    {
        void SetCache(string chave, object value, TimeSpan? expiration);

        object? GetCache(string chave);
    }
}
