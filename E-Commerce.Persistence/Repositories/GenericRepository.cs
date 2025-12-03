using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity) =>_dbContext.Set<TEntity>().Add(entity);

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();
        }

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        { 
            return await SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();
        }



        //public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        //{

        //    // This code breaks OCP because adding any new specification requires modifying (Checking nullability).
        //    // I'll create a helper class to handle these checks so I can extend the logic without editing this code.

        //    IQueryable<TEntity> Query =  _dbContext.Set<TEntity>();
        //    if(specifications is not null)
        //    {
        //        if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
        //        {
        //            foreach (var IncludeExp in specifications.IncludeExpressions)
        //            {
        //                Query = Query.Include(IncludeExp);
        //            }
        //        }
        //    }
        //    return await Query.ToListAsync();
        //}



        public async Task<TEntity?> GetByIdAsync(TKey Id) => await _dbContext.Set<TEntity>().FindAsync(Id);

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationsEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
        
    }
}
