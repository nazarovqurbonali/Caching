namespace RedisCaching.Services.RedisCacheService;

public interface IRedisCacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;
    Task AddAsync<T>(string key, T entity, DateTimeOffset expirationTime, CancellationToken cancellationToken = default)
        where T : class;
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}