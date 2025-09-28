// ADDED
using Microsoft.AspNetCore.Identity;

namespace RealEstateWeb.Models
{
    // Kế thừa IdentityUser để mở rộng khi cần (FullName, Avatar, ...)
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
