using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal static class ProductSpecificationsHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParams queryParams)
        {
            return p => (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId)
            && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()));
        }


    }
}
