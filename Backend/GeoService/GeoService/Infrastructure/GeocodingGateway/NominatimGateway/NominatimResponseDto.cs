using Newtonsoft.Json;

namespace GeoService.Infrastructure.GeocodingGateway.NominatimGateway;

public class NominatimResponseDto
{
    [JsonProperty("place_id")]
    public string PlaceId { set; get; }
    
    [JsonProperty("lat")]
    public string Latitude { set; get; }
    
    [JsonProperty("lon")]
    public string Longtitude { set; get; }
    
    [JsonProperty("name")]
    public string Name { set; get; }
    
    [JsonProperty("addresstype")]
    public string AddressType { set; get; }
    
    [JsonProperty("display_name")]
    public string DisplayName { set; get; }
}