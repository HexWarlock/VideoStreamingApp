using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace VideoStreamingApp.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "EndUser", "ContentProvider", "Moderator" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create a default admin user
            var adminUser = new IdentityUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            string adminPassword = "Admin@123";

            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Moderator");
                }
            }
        }
        public static async Task ClearUserData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.ExecuteSqlRaw("DELETE FROM AspNetUsers;");
            context.Database.ExecuteSqlRaw("DELETE FROM AspNetUserRoles;");
            context.Database.ExecuteSqlRaw("DELETE FROM AspNetUserClaims;");
            context.Database.ExecuteSqlRaw("DELETE FROM AspNetUserLogins;");
            context.Database.ExecuteSqlRaw("DELETE FROM AspNetUserTokens;");
            await context.SaveChangesAsync();
        }
    }
}
               