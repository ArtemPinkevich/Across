using GeoService.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GeoService.Services;

public class PlacesService:IPlacesService
{
    private const int MaxCities = 100;
    
    private readonly GeoDbContext _geoDbContext;
    
    public PlacesService(GeoDbContext geoDbContext)
    {
        _geoDbContext = geoDbContext;
    }
    public IEnumerable<string> GetAllCountries()
    {
        return _geoDbContext.Countries
            .Select(x => x.Name)
            .ToList();
    }

    public IEnumerable<string> GetCountries(string startsWith)
    {
        return _geoDbContext.Countries
            .Select(x => x.Name)
            .Where(x => x.StartsWith(startsWith))
            .ToList();
    }

    public IEnumerable<string> GetAllCities()
    {
        return _geoDbContext.Cities
            .Select(x => x.Name)
            .Take(MaxCities)
            .ToList();
    }

    public IEnumerable<string> GetCities(string startsWith)
    {
        return _geoDbContext.Cities
            .Select(x => x.Name)
            .Where(x => x.StartsWith(startsWith))
            .Take(MaxCities)
            .ToList();
    }

    public IEnumerable<string> GetCitiesByCountry(string country)
    {
        return _geoDbContext.Countries
            .Include(x => x.Cities)
            .FirstOrDefault(x => x.Name == country)!
            .Cities
            .Select(x => x.Name)
            .ToList();
    }
    
    public IEnumerable<string> GetCitiesByCountry(string country, string startsWith)
    {
        return GetCitiesByCountry(country).Where(x => x.StartsWith(startsWith)).ToList();
    }
}