using AutoMapper;
using ECommerce.Services.ShoppingCartAPI.Models;
using ECommerce.Services.ShoppingCartAPI.Models.Dtos;

namespace ECommerce.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
