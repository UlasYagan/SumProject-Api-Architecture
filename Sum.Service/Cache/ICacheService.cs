using System;
using System.Threading.Tasks;

namespace Sum.Service.Cache
{
    public interface ICacheService
    {
        byte[] Get(string key);
        Task<byte[]> GetAsync(string key);
        void Set(string key, byte[] value);
        Task SetAsync(string key, byte[] value);
        void Remove(string key);
        Task RemoveAsync(string key);
        void SetString<T>(string key, T data, TimeSpan? expireTime) where T : class;
        Task SetStringAsync<T>(string key, T data, TimeSpan? expireTime) where T : class;
        T GetString<T>(string key) where T : class;
        Task<T> GetStringAsync<T>(string key) where T : class;
    }
}