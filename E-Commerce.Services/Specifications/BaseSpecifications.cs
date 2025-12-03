using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{

    // hwa zy container bs ll implementation 3shan kda abstract
    internal abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        { //To make it Required not optional
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; }


        #region Include 
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExp)
        {
            IncludeExpressions.Add(IncludeExp);   // due to using of getter-only property so the collection type cannot be modified externally.

        }
        #endregion


        #region orderBy

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderbyExp)
        {
            OrderBy = orderbyExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderbyExp)
        {
            OrderBy = orderbyExp;
        }
        #endregion



        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int PageIndex , int PageSize)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
        

        
    }
}
