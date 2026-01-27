using E_Commerce.Domain.Contracts;
using E_Commerce.Services_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        public async Task<string?> GetAsync(string CachedKey)
        {
            return await _cacheRepository.GetAsync(CachedKey);
        }

        public async Task SetAsync(string CachedKey, object CachedValue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(CachedValue, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cacheRepository.setAsync(CachedKey, Value, TimeToLive); ;
        }
    }
}
