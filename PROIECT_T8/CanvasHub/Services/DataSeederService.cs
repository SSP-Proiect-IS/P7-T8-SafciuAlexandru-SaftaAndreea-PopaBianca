using CanvasHub.Models;
using CanvasHub.Repositories.Interfaces;
using CanvasHub.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CanvasHub.Services
{
    public class DataSeederService : IDataSeederService
    {
        private readonly IServiceProvider _serviceProvider;

        public DataSeederService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SeedRolesAndAdminAsync()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = _serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "User", "Member", "Administrator" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new User
            {
                UserName = "administrator@canvashub.com",
                Email = "administrator@canvashub.com"
            };
    
            string adminPassword = "CanvasHubRules123!";
            var user = await userManager.FindByEmailAsync("administrator@canvashub.com");
            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    adminUser.EmailConfirmed = true;   
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }
        }
    }
}
