using DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsersAsync(userManager);
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                PhoneNumber = "+370622222",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var password = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = password.HashPassword(user, "123");

            var result = await userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = role
                });
            }
        }
    }
}
