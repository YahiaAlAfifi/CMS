using CMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Data
{
    public static class DbSeeder
    {
        public static IHost SeedDb(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();

            try
            {
                var context =
                    scope.ServiceProvider.GetRequiredService<CMSDbContext>();

                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                context.SeedCategory().Wait();
                userManager.SeedUser().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return webHost;
        }

        public static async Task SeedCategory(this CMSDbContext db)
        {
            if (await db.Categories.AnyAsync())
            {
                return;
            }

            var categories = new List<Category>();

            var category1 = new Category
            {
                Name = "A1",
                CreatedAt = DateTime.Now
            };

            var category2 = new Category
            {
                Name = "A2",
                CreatedAt = DateTime.Now
            };

            categories.Add(category1);
            categories.Add(category2);

            await db.Categories.AddRangeAsync(categories);
            await db.SaveChangesAsync();
        }

        public static async Task SeedUser(
            this UserManager<User> userManager)
        {
            if (await userManager.Users.AnyAsync(
                x => x.Email == "dev@gmail.com"))
            {
                return;
            }

            var user = new User
            {
                FullName = "System Developer",
                UserName = "dev@gmail.com",
                Email = "dev@gmail.com",
                CreatedAt = DateTime.Now
            };

            await userManager.CreateAsync(user, "Admin111$$");
        }
    }
}