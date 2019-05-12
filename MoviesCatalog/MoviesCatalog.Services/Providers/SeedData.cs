using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;
using System.Linq;

namespace MoviesCatalog.Services.Providers
{
    public static class SeedData
    {
        public static void SeedDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MoviesCatalogContext>();

                if (dbContext.Roles.Any(u => u.Name == "Admin"))
                {
                    return;
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();

                var adminUser = new ApplicationUser { UserName = "Admin", Email = "admin@admin.admin" };
                userManager.CreateAsync(adminUser, "Admin123@").Wait();

                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            }
        }
    }
}
