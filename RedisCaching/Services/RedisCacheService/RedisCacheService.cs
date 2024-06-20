using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCaching.Services.RedisCacheService;

public class RedisCacheService(IDistributedCache distributedCache):IRedisCacheService
{
    public async Task AddAsync<T>(string key, T entity, DateTimeOffset expirationTime,CancellationToken cancellationToken = default) where T : class
    {
            var cacheOption = new DistributedCacheEntryOptions{ AbsoluteExpiration = expirationTime };
            var jsonSerializerOption = new JsonSerializerOptions() { WriteIndented = true };
            var jsonObject = JsonSerializer.Serialize(entity, jsonSerializerOption);
            await distributedCache.SetStringAsync(key, jsonObject, cacheOption, cancellationToken);
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        
            var dataInCache = await distributedCache.GetStringAsync(key, cancellationToken);

            if (!string.IsNullOrEmpty(dataInCache))
            {

                return JsonSerializer.Deserialize<T>(dataInCache);
            }
            return default;
      
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
            await distributedCache.RemoveAsync(key, cancellationToken);
    }
}