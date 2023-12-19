namespace DataAccess.BaseImplementation
{
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public static class DbRolesInitializer
    {
        public static async Task InitDbRoles(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(UserRoles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (await roleManager.FindByNameAsync(UserRoles.MobileClient) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.MobileClient));
            }
            if (await roleManager.FindByNameAsync(UserRoles.Owner) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Owner));
            }
        }
    }
}
