using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace UseCases.Handlers.Common.Extensions;

public static class UserManagerExtension
{
    public static async Task<string> GetUserRole(this UserManager<User> userManager, User user)
    {
        if (await userManager.IsInRoleAsync(user, UserRoles.Admin))
            return UserRoles.Admin;

        if (await userManager.IsInRoleAsync(user, UserRoles.Owner))
            return UserRoles.Owner;

        if (await userManager.IsInRoleAsync(user, UserRoles.Driver))
            return UserRoles.Driver;

        if (await userManager.IsInRoleAsync(user, UserRoles.Shipper))
            return UserRoles.Shipper;

        if (await userManager.IsInRoleAsync(user, UserRoles.Lawyer))
            return UserRoles.Lawyer;
        return null;
    }
}