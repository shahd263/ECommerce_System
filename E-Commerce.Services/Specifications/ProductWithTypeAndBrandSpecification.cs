using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithTypeAndBrandSpecification(int Id):base(p=>p.Id == Id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) :
            base(ProductSpecificationsHelper.GetProductCriteria(queryParams))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);


            switch (queryParams.Sort)
            {
                case ProductOrderByOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductOrderByOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                        break;
                case ProductOrderByOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductOrderByOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;

            }

            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
        }
    }
}

