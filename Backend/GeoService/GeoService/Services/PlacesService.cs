using GeoService.DataAccess;
using GeoService.Dto;
using GeoService.Infrastructure.GeocodingGateway;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GeoService.Entities;

namespace GeoService.Services;

public class PlacesService:IPlacesService
{
    private const int MaxCities = 5;
    
    private readonly GeoDbContext _geoDbContext;
    private readonly IGeocoderGateway _geocoderGateway;
    
    public PlacesService(GeoDbContext geoDbContext,
        IGeocoderGateway geocoderGateway)
    {
        _geoDbContext = geoDbContext;
        _geocoderGateway = geocoderGateway;
    }

    public async Task<IEnumerable<PlaceDto>> GetPlaces(string startsWith)
    {
        var start = startsWith.ToLower();
        var cities = await _geoDbContext.Cities
            .Where(x => EF.Functions.Like(x.Name, $"{start}%"))
            .Include(x => x.Country)
            .Include(x => x.Region)
            .Take(MaxCities)
            .ToListAsync();
        
        return await ConvertToPlaceDto(cities).ToListAsync();
    }

    private async IAsyncEnumerable<PlaceDto> ConvertToPlaceDto(IEnumerable<City> cities)
    {
        foreach (var city in cities)
        {
            var result = await _geocoderGateway.FindCoordinates(city.Name);
            yield return new PlaceDto()
            {
                Country = city.Country == null ? "" : FirstCharToUpper(city.Country.Name),
                Region = city.Region == null ? "" : FirstCharToUpper(city.Region.Name),
                City = FirstCharToUpper(city.Name),
                Latitide = result.Latitude,
                Longtitude = result.Longtitude,
                MapDisplayName = result.DisplayName
            };
        }
    }

    private string FirstCharToUpper(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        return $"{char.ToUpper(input[0])}{input[1..]}";
    }
}