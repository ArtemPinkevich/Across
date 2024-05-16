using GeoService.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GeoService.Extensions;

public static class DatabaseExtension
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        //db pool context использовать пул контекстов для опитимизации
        services.AddDbContext<GeoDbContext>(opt => opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
            .LogTo(Console.WriteLine, LogLevel.Debug));
    }
}