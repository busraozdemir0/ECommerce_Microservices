using AutoMapper;
using ECommerce.MessageBus;
using ECommerce.Services.ShoppingCartAPI.Data;
using ECommerce.Services.ShoppingCartAPI.Models;
using ECommerce.Services.ShoppingCartAPI.Models.Dtos;
using ECommerce.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace ECommerce.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;
        private ICouponService _couponService;
        private IMessageBus _messageBus;
        private IConfiguration _configuration;

        public CartAPIController(IMapper mapper, AppDbContext db, IProductService productService, ICouponService couponService, IMessageBus messageBus, IConfiguration configuration)
        {
            _mapper = mapper;
            _db = db;
            this._response = new ResponseDTO();
            _productService = productService;
            _couponService = couponService;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDTO> GetCart(string userId)
        {
            try
            {
                CartDTO cart = new() // Giris yapan kullanicinin Sepet bilgileri icin CartHeaders tablosundan kaydini bul
                {
                    CartHeader = _mapper.Map<CartHeaderDTO>(_db.CartHeaders.First(u => u.UserId == userId))
                };
                // CartHeader tablosundan bulunan kayit ile CartDetails tablosunda o kullaniciya ait olan tum kayitlari getir (yani sepete ekledigi tum urunler gelecek)
                cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDTO>>(_db.CartDetails
                    .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId));

                // Sepette olan tum urunleri getir.
                IEnumerable<ProductDTO> productDTOs = await _productService.GetProducts();

                // Sepetin tuttugu toplam ucret icin sepette yer alan tum urunleri miktarlari ile carpiyoruz. Ve her bir urun icin sonucu CartTotal degiskeninde topluyoruz.
                foreach (var item in cart.CartDetails)
                {
                    // Sepette olan urunler icerisindeki ProductId'si CartDetails'teki ProductId'ye esit olani o an uzerinde bulunan item'in Product'ina ata.
                    item.Product = productDTOs.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                // Kupon varsa kuponu uygula
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDTO coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;  // Kupondaki indirim miktari kadar toplam fiyattan cikar
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("ApplyCoupon")] // Kupon uygulama islemi
        public async Task<ResponseDTO> ApplyCoupon([FromBody] CartDTO cartDTO)
        {
            try
            {
                // Gelen karttaki CartHeader'daki UserId bilgisi CartHeader tablosundaki kayitlardan herhangi biriyle esleseni bul
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserId == cartDTO.CartHeader.UserId);
                cartFromDb.CouponCode = cartDTO.CartHeader.CouponCode; // CartDTO'dan gelen kupon kodunu bulunan kayittaki CouponCode'una aktar
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }
        [HttpPost("RemoveCoupon")] // Kuponu kaldirma islemi
        public async Task<ResponseDTO> RemoveCoupon([FromBody] CartDTO cartDTO)
        {
            try
            {
                var cartFromDb = await _db.CartHeaders.FirstAsync(u => u.UserId == cartDTO.CartHeader.UserId);
                cartFromDb.CouponCode = "";
                _db.CartHeaders.Update(cartFromDb);
                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("EmailCartRequest")] 
        public async Task<ResponseDTO> EmailCartRequest([FromBody] CartDTO cartDTO)
        {
            try
            {
                await _messageBus.PublishMessage(cartDTO, _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue"));
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> CartUpsert(CartDTO cartDTO)
        {
            try
            {
                // Giris yapan kullanicinin sepette bir urunu var mi
                var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == cartDTO.CartHeader.UserId);

                if (cartHeaderFromDb == null) // Eger baslik bos ise (yani kullanicinin CartHeader tablosunda bilgisi/UserId yoksa)
                {
                    // Sepet basligi ve detaylari olustur (CartHeader and CartDetails)
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDTO.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();

                    cartDTO.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    // Baslik (cartHeader) bos degilse
                    // Detaylarin ayni urune sahip olup olmadigini kontrol ediyoruz
                    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cartDTO.CartDetails.First().ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if (cartDetailsFromDb == null) // Eger ayni urun sepette yoksa
                    {
                        // CartDetails olustur / yani urunu sepete ekle
                        cartDTO.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else // Urun sepette varsa urun miktari arttirilacak
                    {
                        // Sepette urunun miktarini arttir
                        cartDTO.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDTO.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDTO.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                        await _db.SaveChangesAsync();

                    }
                }
                _response.Result = cartDTO;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDTO> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                // CartDetails tablosunda cartDetailsId ile eslesen ilk kaydi bulur.
                CartDetails cartDetails = _db.CartDetails
                    .First(u => u.CartDetailsId == cartDetailsId);

                // Bulunan cartDetails nesnesinin CartHeaderId ile eslesen tum kayitlari sayar
                int totalCountofCartItem = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails); // Bulunan cartDetails kaydini db'den siler

                if (totalCountofCartItem == 1) // Eger bu sepet basligina ait yalnizca bir adet CartDetails kaydi varsa
                {
                    var cartHeaderToRemove = await _db.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId); // cartDetails nesnesinin CartHeaderId ile eslesen ilk kaydini bulur

                    _db.CartHeaders.Remove(cartHeaderToRemove); // Ardindan bulunan cartHeader kaydini db'den siler
                }

                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
