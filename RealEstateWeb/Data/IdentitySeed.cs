
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealEstateWeb.Models;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = new[] { "Admin", "User" };
        foreach (var r in roles)
            if (!await roleMgr.RoleExistsAsync(r)) await roleMgr.CreateAsync(new IdentityRole(r));

        var adminEmail = "admin@example.com";
        var admin = await userMgr.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser { UserName = "admin", Email = adminEmail, EmailConfirmed = true, FullName = "Site Admin" };
            var res = await userMgr.CreateAsync(admin, "Admin123!");
            if (res.Succeeded) await userMgr.AddToRoleAsync(admin, "Admin");
        }
    }
}
