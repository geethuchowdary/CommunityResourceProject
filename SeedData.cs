using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommunityResource
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var adminAlreadyExists = await roleManager.RoleExistsAsync(Constants.AdminRole);
            if (adminAlreadyExists) { return; }
            await roleManager.CreateAsync(new IdentityRole(Constants.AdminRole));
        }

        public static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "useradmin1@gmail.com").SingleOrDefaultAsync();
            
            if (testAdmin == null) { return; };
            testAdmin = new IdentityUser { UserName = "useradmin1@gmail.com", Email = "useradmin1@gmail.com" };
            await userManager.CreateAsync(testAdmin, "Useradmin@1");

            await userManager.AddToRoleAsync(testAdmin, Constants.AdminRole);
        }
    }
}
