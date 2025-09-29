namespace RealEstateWeb.Models.ViewModels.Manage
{
    public class ManageProfileViewModel
    {
        public string UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public IFormFile? AvatarFile { get; set; } 

        //  đổi mật khẩu
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
