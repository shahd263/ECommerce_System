using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

       

        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO Basket)
        {
            var customerBasket =  _mapper.Map<CustomerBasket>(Basket);
            var CreatedOrDeletedBasket = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            return _mapper.Map<BasketDTO>(CreatedOrDeletedBasket);
        }

        public async Task<bool> DeleteAsync(string Id) => await _basketRepository.DeleteBasketAsync(Id);


        public async Task<BasketDTO> GetBasketAsync(string Id)
        {
            var Basket =await _basketRepository.GetBasketAsync(Id);
            return _mapper.Map<BasketDTO>(Basket);
        }
    }
}
