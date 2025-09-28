namespace RealEstateWeb.Models.ViewModels.Admin
{
    public class EditUserRolesViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<RoleItem> AllRoles { get; set; } = new();
    }

    public class RoleItem
    {
        public string RoleName { get; set; } = string.Empty;
        public bool Selected { get; set; }
    }
}
