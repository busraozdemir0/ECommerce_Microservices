using AutoMapper;
using ECommerce.Services.ProductAPI.Models;
using ECommerce.Services.ProductAPI.Models.Dtos;

namespace ECommerce.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
