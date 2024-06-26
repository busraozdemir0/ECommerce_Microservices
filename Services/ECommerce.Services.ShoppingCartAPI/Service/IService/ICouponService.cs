using ECommerce.Services.ShoppingCartAPI.Models.Dtos;

namespace ECommerce.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}
