namespace GeoService.Services;

public interface IPlacesService
{
    IEnumerable<string> GetAllCountries();
    
    IEnumerable<string> GetCountries(string startsWith);
    
    IEnumerable<string> GetAllCities();
    
    IEnumerable<string> GetCities(string startsWith);
    
    IEnumerable<string> GetCitiesByCountry(string country);
    
    IEnumerable<string> GetCitiesByCountry(string country, string startsWith);
}