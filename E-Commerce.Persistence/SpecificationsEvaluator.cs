using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal static class SpecificationsEvaluator
    {
        //Create Query 
        //_dbContext.Products.Include(p=>p.ProductBrand).Include(p=>p.ProductType)

        public static IQueryable<TEntity>  CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint , ISpecifications<TEntity,TKey> specifications )
            where TEntity : BaseEntity<TKey>
        {
            var Query = EntryPoint; //_dbContext.Products
            if(specifications is not null)
            {

                if (specifications.Criteria is not null)
                {
                    Query = Query.Where(specifications.Criteria);
                }

                if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    //aggregate operations are operations that process a collection (like a list, array, or IQueryable) and combine all its elements into a single final result.
                    Query = specifications.IncludeExpressions.Aggregate(Query,
                        (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
                    //Query.Include(p=>p.ProductBrand).Include(p=>p.ProductType)
                }

                if(specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }

                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderBy(specifications.OrderByDescending);
                }

                if (specifications.IsPaginated)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }






            }
            return Query;
        }
    }
}
