using GeoService.Services;

namespace GeoService.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPlacesService, PlacesService>();
    }
}