// Data/IdentitySeed.cs  (CREATE or REPLACE)
using Microsoft.AspNetCore.Identity;
using RealEstateWeb.Models;

namespace RealEstateWeb.Data
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // roles to ensure
            string[] roles = new[] { "Admin", "User" };

            foreach (var r in roles)
            {
                if (!await roleMgr.RoleExistsAsync(r))
                {
                    var role = new IdentityRole(r);
                    var result = await roleMgr.CreateAsync(role);
                    // optionally log result
                }
            }

            // create admin user if not exists
            var adminEmail = "admin@example.com";
            var adminUser = await userMgr.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "Site Admin"
                };

                var res = await userMgr.CreateAsync(adminUser, "Admin123!"); // change password if you want
                if (res.Succeeded)
                {
                    await userMgr.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    // optionally explore errors and log
                }
            }
            else
            {
                // ensure has Admin role
                if (!await userMgr.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userMgr.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
