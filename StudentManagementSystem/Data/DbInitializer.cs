using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Tạo Role "Admin" nếu chưa tồn tại
            string adminRole = "Admin";
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Tạo người dùng Admin nếu chưa tồn tại
            string adminEmail = "pvn19299@gmail.com";
            string adminPassword = "pvn19299"; // Đặt mật khẩu mạnh hơn trong thực tế!

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    // Gán người dùng vào Role "Admin"
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}