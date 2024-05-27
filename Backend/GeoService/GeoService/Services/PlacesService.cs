using GeoService.DataAccess;
using GeoService.Dto;
using Microsoft.EntityFrameworkCore;

namespace GeoService.Services;

public class PlacesService:IPlacesService
{
    private const int MaxCities = 20;
    
    private readonly GeoDbContext _geoDbContext;
    
    public PlacesService(GeoDbContext geoDbContext)
    {
        _geoDbContext = geoDbContext;
    }

    public IEnumerable<PlaceDto> GetPlaces(string startsWith)
    {
        var start = startsWith.ToLower();
        var cities = _geoDbContext.Cities
            .Where(x => EF.Functions.Like(x.Name, $"{start}%"))
            .Include(x => x.Country)
            .Include(x => x.Region)
            .Take(MaxCities)
            .ToList();

        IEnumerable<PlaceDto> places = cities.Select(x => new PlaceDto()
        {
            Country = x.Country == null ? "" : FirstCharToUpper(x.Country.Name),
            Region = x.Region == null ? "" : FirstCharToUpper(x.Region.Name),
            City = FirstCharToUpper(x.Name),
        });

        return places;
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