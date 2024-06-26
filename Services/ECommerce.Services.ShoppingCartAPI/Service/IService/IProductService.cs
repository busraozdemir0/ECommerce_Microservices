using ECommerce.Services.ShoppingCartAPI.Models.Dtos;

namespace ECommerce.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
