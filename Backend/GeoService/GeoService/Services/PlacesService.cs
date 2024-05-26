using GeoService.DataAccess;
using GeoService.Dto;
using Microsoft.EntityFrameworkCore;

namespace GeoService.Services;

public class PlacesService:IPlacesService
{
    private const int MaxCities = 50;
    
    private readonly GeoDbContext _geoDbContext;
    
    public PlacesService(GeoDbContext geoDbContext)
    {
        _geoDbContext = geoDbContext;
    }

    public IEnumerable<PlaceDto> GetPlaces(string startsWith)
    {
        var cities = _geoDbContext.Cities
            .Where(x => x.Name.StartsWith(startsWith))
            .Include(x => x.Country)
            .Include(x => x.Region)
            .Take(MaxCities)
            .ToList();

        IEnumerable<PlaceDto> places = cities.Select(x => new PlaceDto()
        {
            Country = x.Country == null ? "" : x.Country.Name,
            Region = x.Region == null ? "" : x.Region.Name,
            City = x.Name,
        });
        
        return new List<PlaceDto>()
        {
            new PlaceDto()
            {
                Country = "Россия",
                City = "Москва",
                Region = "Московская обл"
            },
            new PlaceDto()
            {
                Country = "Россия",
                City = "Томск",
                Region = "Томска обл"
            },
            new PlaceDto()
            {
                Country = "Россия",
                City = "Новосибирск",
                Region = "Ноовосибирская обл"
            },
            new PlaceDto()
            {
                Country = "Казахстан",
                City = "Астана",
                Region = "Акмоолинская обл"
            },
            new PlaceDto()
            {
                Country = "Казахстан",
                City = "Алматы",
                Region = "Алматинская обл"
            },
            new PlaceDto()
            {
                Country = "Казахстан",
                City = "Павлодар",
                Region = "Павлодарская обл"
            },
        };
    }
}