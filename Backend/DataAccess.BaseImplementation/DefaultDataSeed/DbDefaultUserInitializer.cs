namespace DataAccess.BaseImplementation
{
    using DataAccess.Interfaces;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class DbDefaultUserInitializer
    {
        public static async Task InitDbUser(UserManager<User> userManager)
        {

        }
    }
}
