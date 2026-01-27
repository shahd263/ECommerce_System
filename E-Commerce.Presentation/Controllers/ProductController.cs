using E_Commerce.Presentation.Attributes;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController :ControllerBase
    {
        private readonly IProdcutService _prodcutService;

        public ProductController(IProdcutService prodcutService)
        {
            _prodcutService = prodcutService;
        }

        [HttpGet]
        [RedisCache]
        //GET :BaseUrl/api/Products
                            // to can detect what to return in happy scenario
        public async Task<ActionResult<PagintaedResult<ProductDTO>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var Products = await _prodcutService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }


        [HttpGet("{id}")]
        //GET :BaseUrl/api/Products/1

        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var Product = await _prodcutService.GetProductByIdAsync(id);
            return Ok(Product);
        }



        [HttpGet("brands")]
        //GET :BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var Brands = await _prodcutService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("types")]
        //GET :BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAlltypes()
        {
            var Types = await _prodcutService.GetAllTypesAsync();
            return Ok(Types);
        }
    }
}
