using ECommerce.Services.Models;
using ECommerce.Web.Models;

namespace ECommerce.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true);
    }
}
