namespace Ecommerce.Infrastructure.Interfaces
{
    public interface ICacheRepositroy
    {
        Task<T> GetDataAsync<T>(string key);
        Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime);
        Task<bool> RemoveDataAsync(string key);
    }
}
