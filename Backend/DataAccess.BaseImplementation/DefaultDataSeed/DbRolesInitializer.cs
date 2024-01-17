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
            
            if (await roleManager.FindByNameAsync(UserRoles.Shipper) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Shipper));
            }
            
            if (await roleManager.FindByNameAsync(UserRoles.Driver) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Driver));
            }
            
            if (await roleManager.FindByNameAsync(UserRoles.Lawyer) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Lawyer));
            }
            
            if (await roleManager.FindByNameAsync(UserRoles.Owner) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Owner));
            }
        }
    }
}
