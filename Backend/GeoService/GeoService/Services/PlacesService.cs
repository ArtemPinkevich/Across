using GeoService.DataAccess;
using GeoService.Dto;
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

    public IEnumerable<PlaceDto> GetPlaces(string startsWith)
    {
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