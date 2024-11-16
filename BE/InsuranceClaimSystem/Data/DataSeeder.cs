using InsuranceClaimSystem.Constants;
using InsuranceClaimSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace InsuranceClaimSystem.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Define roles
            var roles = new[] { Roles.ROLE_ADMIN, Roles.ROLE_USER };

            // Create roles if they don't exist
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create Admin user
            var adminEmail = "admin@gmail.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new AppUser
                {
                    FullName = "Duy Admin",
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var adminPassword = "Admin@123";

                var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.ROLE_ADMIN);
                }
            }

            // Create User
            string[] userEmails = { "user01@gmail.com", "user02@gmail.com", "user03@gmail.com" };
            foreach (var userEmail in userEmails)
            {
                if (await userManager.FindByEmailAsync(userEmail) == null)
                {
                    var normalUser = new AppUser
                    {
                        FullName = "User " + userEmail,
                        UserName = userEmail,
                        Email = userEmail,
                        EmailConfirmed = true
                    };
                    var userPassword = "User@123";

                    var createUserResult = await userManager.CreateAsync(normalUser, userPassword);
                    if (createUserResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(normalUser, Roles.ROLE_USER);
                    }
                }
            }
        }
    }
}
