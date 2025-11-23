using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsyn(TKey Id);
        void Add(TEntity entity); // No need to be Async because the operation occurs in database only when i saveChanges

        void Update(TEntity entity);
        void Delete(TEntity entity); 
        //There is no Async version for Update & Delete 
        // Because it changes locally 
    }
}
