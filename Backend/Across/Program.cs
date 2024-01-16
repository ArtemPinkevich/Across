using System.Threading.Tasks;
using DataAccess.BaseImplementation;
using DataAccess.Interfaces;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Across
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<DatabaseContext>();
                await dbContext.Database.MigrateAsync();
                var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var carWashesRepository = services.GetRequiredService<IRepository<CarWash>>();
                await DbRolesInitializer.InitDbRoles(rolesManager);
                await DbDefaultUserInitializer.InitDbUser(userManager, carWashesRepository);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://0.0.0.0:5000");
                    //webBuilder.UseUrls("http://0.0.0.0:5000",
                    //    "https://0.0.0.0:5001");
                });
    }
}
