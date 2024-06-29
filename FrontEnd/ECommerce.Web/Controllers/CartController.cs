using ECommerce.Services.Models;
using ECommerce.Web.Models;
using ECommerce.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDTOBaseOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value; // Giris yapan kullanicinin UserId bilgisi cekiliyor
            ResponseDTO response = await _cartService.RemoveFromCartAsync(cartDetailsId); // Sepette olan ve silinmek istenen urunu silecek
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Sepetiniz başarıyla güncellendi.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
        {
            ResponseDTO response = await _cartService.ApplyCouponAsync(cartDTO);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Kupon başarıyla uygulandı.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
        {
            cartDTO.CartHeader.CouponCode = "";
            ResponseDTO response = await _cartService.ApplyCouponAsync(cartDTO);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Kupon başarıyla kaldırıldı.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDTO cartDTO)
        {
            CartDTO cart = await LoadCartDTOBaseOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            ResponseDTO response = await _cartService.EmailCart(cartDTO);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "E-posta kısa süre içinde işlenecek ve gönderilecek.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        // * Giris yapan kullanicinin sepete ekledigi urunleri listeleyecek olan fonksiyon
        private async Task<CartDTO> LoadCartDTOBaseOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value; // Giris yapan kullanicinin UserId bilgisi cekiliyor
            ResponseDTO response = await _cartService.GetCartByUserIdAsync(userId); // userId bilgisine gore sepeti getir
            if (response != null & response.IsSuccess)
            {
                CartDTO cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
                return cartDTO;
            }
            return new CartDTO();
        }
    }
}
