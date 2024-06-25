using Microsoft.AspNetCore.Identity;

namespace ECommerce.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
