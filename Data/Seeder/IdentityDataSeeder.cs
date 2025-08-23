using LmsProject.Models;
using Microsoft.AspNetCore.Identity;

namespace LmsProject.Data.Seeder
{
    public class IdentityDataSeeder
    {
        public static async Task SeedRolesAndAdminUserAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Teacher", "Student" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            // Seed a default Admin user
            var seedUsers = new List<ApplicationUserSeedModel>
{
                // Admins
                new ApplicationUserSeedModel { UserName = "admin@example.com", Email = "admin@example.com", Password = "Admin123!", Role = "Admin" },
                new ApplicationUserSeedModel { UserName = "admin2@example.com", Email = "admin2@example.com", Password = "Admin123!", Role = "Admin" },
                new ApplicationUserSeedModel { UserName = "admin3@example.com", Email = "admin3@example.com", Password = "Admin123!", Role = "Admin" },

                // Teachers
                new ApplicationUserSeedModel { UserName = "teacher1@example.com", Email = "teacher1@example.com", Password = "Teacher123!", Role = "Teacher" },
                new ApplicationUserSeedModel { UserName = "teacher2@example.com", Email = "teacher2@example.com", Password = "Teacher123!", Role = "Teacher" },
                new ApplicationUserSeedModel { UserName = "teacher3@example.com", Email = "teacher3@example.com", Password = "Teacher123!", Role = "Teacher" },

                // Students
                new ApplicationUserSeedModel { UserName = "student1@example.com", Email = "student1@example.com", Password = "Student123!", Role = "Student" },
                new ApplicationUserSeedModel { UserName = "student2@example.com", Email = "student2@example.com", Password = "Student123!", Role = "Student" },
                new ApplicationUserSeedModel { UserName = "student3@example.com", Email = "student3@example.com", Password = "Student123!", Role = "Student" },
            };

            foreach (var userData in seedUsers)
            {
                var existingUser = await userManager.FindByEmailAsync(userData.Email);
                if (existingUser == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = userData.UserName,
                        Email = userData.Email,
                        EmailConfirmed = true,
                    };

                    var result = await userManager.CreateAsync(newUser, userData.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, userData.Role);
                    }
                }
            }
        }

    }
}
