using AutoMapper;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfiles
{

    // any value with logic need Value Resolver not only the URLs  
    internal class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        //no need for DI because it is already injected and used in program 
        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;


            //From Browser not local
            if (source.PictureUrl.StartsWith("http")) return source.PictureUrl;

            var BaseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            var PicUrl = $"{BaseUrl}{source.PictureUrl}";

            return PicUrl;
        }
    }
}
