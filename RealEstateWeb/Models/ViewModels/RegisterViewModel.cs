// ADDED
using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? FullName { get; set; }

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
