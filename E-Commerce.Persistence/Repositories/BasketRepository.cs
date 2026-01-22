using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer Connection)
        {
            _database = Connection.GetDatabase();
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan TimeToLive = default)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(Basket.Id, JsonBasket, (TimeToLive == default) ? TimeSpan.FromDays(7) : TimeToLive);

            if (IsCreatedOrUpdated)
            {
                return await GetBasketAsync(Basket.Id);
            }
            return null;

        }
        public async Task<bool> DeleteBasketAsync(string BasketId) => await _database.KeyDeleteAsync(BasketId);
        

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var Basket = await _database.StringGetAsync(Id);
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
