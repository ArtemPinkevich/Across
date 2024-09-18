using GeoService.Infrastructure.GeocodingGateway;
using GeoService.Infrastructure.GeocodingGateway.NominatimGateway;
using GeoService.Services;

namespace GeoService.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPlacesService, PlacesService>();
        services.AddScoped<IGeocoderGateway, NominatimGateway>();
    }
}