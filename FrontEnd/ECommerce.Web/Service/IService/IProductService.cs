using ECommerce.Services.Models;
using ECommerce.Web.Models;

namespace ECommerce.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetAllProductsAsync();
        Task<ResponseDTO?> GetProductByIdAsync(int id);
        Task<ResponseDTO?> CreateProductAsync(ProductDTO ProductDTO);
        Task<ResponseDTO?> UpdateProductAsync(ProductDTO ProductDTO);
        Task<ResponseDTO?> DeleteProductAsync(int id);
    }
}
