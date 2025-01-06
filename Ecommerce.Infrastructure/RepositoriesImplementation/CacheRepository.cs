using Ecommerce.Infrastructure.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class CacheRepository : ICacheRepositroy
    {
        private readonly IDatabase _cacheDb;

        public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _cacheDb = connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetDataAsync<T>(string key)
        {
            var value = await _cacheDb.StringGetAsync(key);
            return !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<T>(value) : default;
        }

        public async Task<bool> RemoveDataAsync(string key)
        {

            return await _cacheDb.KeyDeleteAsync(key);
        }

        public async Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime - DateTimeOffset.Now;
            return await _cacheDb.StringSetAsync(key, JsonSerializer.Serialize(value), expiryTime);
        }


    }
}
