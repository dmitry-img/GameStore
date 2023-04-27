using System;
using System.Threading.Tasks;
using GameStore.DAL.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace GameStore.DAL.Repositories
{
    public class RedisCacheRepository<T> : IDistributedCache<T>
    {
        private readonly IDatabase _database;

        public RedisCacheRepository(ConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<T> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.Length() == 0)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task SetAsync(string key, T value)
        {
            var valueBytes = JsonConvert.SerializeObject(value);
            await _database.StringSetAsync(key, valueBytes);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
