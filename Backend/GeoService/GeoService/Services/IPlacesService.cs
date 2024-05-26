using GeoService.Dto;

namespace GeoService.Services;

public interface IPlacesService
{
    IEnumerable<PlaceDto> GetPlaces(string startsWith);
}