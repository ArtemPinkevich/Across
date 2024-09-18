using Newtonsoft.Json;

namespace GeoService.Infrastructure.GeocodingGateway.NominatimGateway;

public class NominatimGateway : IGeocoderGateway
{
    private const int DELAY_BETWEEN_FAILED_ATTEMPTS = 2;
    private const int MAX_ATTEMPTS_TO_SEND_FALIED_SMS = 3;

    private readonly HttpClient _httpClient;
    private const string _requestTemplate =
        "https://nominatim.openstreetmap.org/search?q={0}&format={1}&addressdetails=1";

    private const string _responseFormat = "jsonv2";

    public NominatimGateway()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Across service");
        _httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("ru");
    }

    public async Task<Coordinates> FindCoordinates(string locationName)
    {
        var response = await GetNominatimResponse(locationName);
        return GetCoordinatesFromResponse(response, locationName);
    }
    
    
    private async Task<NominatimResponseDto[]> GetNominatimResponse(string locationName)
    {
        await using Stream stream = await _httpClient.GetStreamAsync(CreateUrl(locationName));
        using StreamReader streamReader = new StreamReader(stream);
        await using JsonReader reader = new JsonTextReader(streamReader);
        JsonSerializer serializer = new JsonSerializer();
        return (serializer.Deserialize<NominatimResponseDto[]>(reader) ?? null) ?? throw new InvalidOperationException();
    }
    
    private string CreateUrl(string locationName)
    {
        return String.Format(_requestTemplate, locationName, _responseFormat);
    }

    private Coordinates GetCoordinatesFromResponse(NominatimResponseDto[] responseDto, string location)
    {
        if (responseDto.Length <= 0)
            return new Coordinates() { Latitude = "not found", Longtitude = "not found", };

        var nominatimResponseDto = responseDto.FirstOrDefault(x => x.Name.Equals(location, StringComparison.InvariantCultureIgnoreCase) 
                                                                   && (x.AddressType == "city" || x.AddressType == "town"));
        if (nominatimResponseDto != null)
        {
            return new Coordinates()
            {
                Latitude = nominatimResponseDto.Latitude,
                Longtitude = nominatimResponseDto.Longtitude,
                Name = nominatimResponseDto.Name,
                DisplayName = nominatimResponseDto.DisplayName
            };
        }
        return new Coordinates() { Latitude = "not found", Longtitude = "not found", };
    }
}