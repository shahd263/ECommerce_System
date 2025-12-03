using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface IProdcutService
    {
        Task<PagintaedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDTO?> GetProductByIdAsync(int Id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();



    }
}
