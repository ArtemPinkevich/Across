using GeoService.Infrastructure.GeocodingGateway.NominatimGateway;

namespace GeoService.Infrastructure.GeocodingGateway;

public interface IGeocoderGateway
{
    Task<Coordinates> FindCoordinates(string locationName);
}