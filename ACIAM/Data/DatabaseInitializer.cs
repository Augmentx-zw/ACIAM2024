using Microsoft.AspNetCore.Identity;

namespace ACIAM.Data
{
    public class DatabaseInitializer
    {
        public static async Task SeedDataAsync(UserManager<IdentityUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            if (userManager == null || roleManager == null)
            {
                Console.WriteLine("userManager or roleManager is null");
                return;
            }

            var exists = await roleManager.RoleExistsAsync("admin");
            if (!exists)
            {
                Console.WriteLine("Admin role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            var user = new IdentityUser()
            {
                UserName = "admintest@admin.com",
                Email = "admintest@admin.com"
            };

            string defaultPassword = "Admin@123";

            var result = await userManager.CreateAsync(user, defaultPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
                Console.WriteLine("Admin user created successfully! Please update the initial password!");
                Console.WriteLine("Email: " + user.Email + " - Initial password: " + defaultPassword);
            }
            else
            {
                Console.WriteLine("Unable to create Admin User: " + result.Errors.First().Description);
            }
        }
    }
}
