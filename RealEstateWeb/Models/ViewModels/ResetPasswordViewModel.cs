// ADDED
using System.ComponentModel.DataAnnotations;

namespace RealEstateWeb.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
