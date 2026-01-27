using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cachedKey)
        {
            var cachedValue = await _database.StringGetAsync(cachedKey);
            return cachedValue.IsNullOrEmpty ? null : cachedValue.ToString();
        }

        public async Task setAsync(string CacheKey, string CacheValue, TimeSpan timeToLive)
        {
            await _database.StringSetAsync(CacheKey, CacheValue, timeToLive);

        }
    }
}
