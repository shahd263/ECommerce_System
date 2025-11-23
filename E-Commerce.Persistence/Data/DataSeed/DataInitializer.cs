using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Initialize()
        {
            try
            {
                var HasProducts = _dbContext.Products.Any();
                var HasBrands = _dbContext.ProductBrands.Any();
                var HasTypes = _dbContext.ProductTypes.Any();
                if (HasProducts && HasBrands && HasTypes) return;

                if (!HasBrands)
                    SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);

                if (!HasTypes)
                   SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);

                _dbContext.SaveChanges();

                if (!HasProducts)
                    SeedDataFromJson<Product, int>("products.json", _dbContext.Products);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seed Failed : {ex}");
            }


        }


        public void SeedDataFromJson<T,TKey>(string FileName , DbSet<T> dbSet) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\E-Commerce.Presistence\Data\DataSeed\JSONFiles\" + FileName;

            if (!File.Exists(FilePath)) throw new FileNotFoundException($"File {FileName} is not found");

            try
            {
                using var DataStream = File.OpenRead(FilePath);

                var data = JsonSerializer.Deserialize<List<T>>(DataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (data is not null)
                {
                    dbSet.AddRange(data);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error While Reading Json File : {ex}");
                
            }
            
        }
    }
}
