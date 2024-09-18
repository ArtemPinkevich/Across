using GeoService.Dto;

namespace GeoService.Services;

public interface IPlacesService
{
    Task<IEnumerable<PlaceDto>> GetPlaces(string startsWith);
}