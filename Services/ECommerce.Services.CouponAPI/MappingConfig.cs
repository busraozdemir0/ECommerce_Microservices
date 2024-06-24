using AutoMapper;
using ECommerce.Services.CouponAPI.Models;
using ECommerce.Services.CouponAPI.Models.Dtos;

namespace ECommerce.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>();
                config.CreateMap<Coupon, CouponDTO>();
            });
            return mappingConfig;
        }
    }
}
