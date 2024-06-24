using ECommerce.Services.CouponAPI.Data;
using ECommerce.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CouponAPIController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _context.Coupons.ToList();
                return objList;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        [HttpGet]
        [Route("{id:int}")] // Bu sekilde route'unu belirtmezsek swagger'da hata aliriz.
        public object Get(int id)
        {
            try
            {
                Coupon value = _context.Coupons.First(u => u.CouponId == id);
                return value;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
