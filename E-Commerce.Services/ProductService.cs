using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Services.Exceptions;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProdcutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

        public async Task<PagintaedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            // Specifications -> products with including Product Type and Product Brand 
            var Spec = new ProductWithTypeAndBrandSpecification(queryParams);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Spec);
            var ReturnedData= _mapper.Map<IEnumerable<ProductDTO>>(Products);
            var CountReturnedData = ReturnedData.Count();
            var CountSpec = new ProductCountSpecifications(queryParams);
            var CountAllProduct = await _unitOfWork.GetRepository<Product, int>().CountAsync(CountSpec);
            return new PagintaedResult<ProductDTO>(CountAllProduct, CountReturnedData, queryParams.PageIndex, ReturnedData);

        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int Id)
        {
            var Spec = new ProductWithTypeAndBrandSpecification(Id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Spec);
            if (Product is null) throw new ProductNotFoundException(Id);
            return _mapper.Map<ProductDTO>(Product);
        }
    }
}
