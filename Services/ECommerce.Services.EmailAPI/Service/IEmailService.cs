using ECommerce.Services.EmailAPI.Models.Dtos;

namespace ECommerce.Services.EmailAPI.Service
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDTO cartDTO);
    }
}
