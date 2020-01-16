using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Sum.Service.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IDatabase database)
        {
            _database = database;
        }

        public byte[] Get(string key)
        {
            return _database.StringGet(key);
        }

        public async Task<byte[]> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public T GetString<T>(string key) where T : class
        {
            return JsonConvert.DeserializeObject<T>(_database.StringGet(key).ToString() ?? "");
        }

        public async Task<T> GetStringAsync<T>(string key) where T : class
        {
            var result = await _database.StringGetAsync(key);
            return JsonConvert.DeserializeObject<T>(result.ToString() ?? "");
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public void Set(string key, byte[] value)
        {
            _database.StringSet(key, value);
        }

        public async Task SetAsync(string key, byte[] value)
        {
            await _database.StringSetAsync(key, value);
        }

        public void SetString<T>(string key, T data, TimeSpan? expireTime) where T : class
        {
            if (expireTime != null)
                _database.StringSet(key, JsonConvert.SerializeObject(data), expireTime);
            else
                _database.StringSet(key, JsonConvert.SerializeObject(data));
        }

        public async Task SetStringAsync<T>(string key, T data, TimeSpan? expireTime) where T : class
        {
            if (expireTime != null)
                await _database.StringSetAsync(key, JsonConvert.SerializeObject(data), expireTime);
            else
                await _database.StringSetAsync(key, JsonConvert.SerializeObject(data));
        }
    }
}