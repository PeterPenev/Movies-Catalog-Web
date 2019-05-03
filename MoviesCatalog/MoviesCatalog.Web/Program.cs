﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoviesCatalog.Data;
using MoviesCatalog.Data.Models;

namespace MoviesCatalog.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            SeedDatabase(host);

            host.Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void SeedDatabase(IWebHost host)
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

        public static IWebHost BuildWebHost(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
              .Build();
    }
}
