using GeoService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoService.Controllers;

//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly ILogger<PlacesController> _logger;
    private readonly IPlacesService _placesService;

    public PlacesController(ILogger<PlacesController> logger,
        IPlacesService placesService)
    {
        _logger = logger;
        _placesService = placesService;
    }

    //[Authorize(Roles = $"Driver,Shipper")]
    [HttpGet("get_all_countries")]
    public IEnumerable<string> GetCountries()
    {
        return _placesService.GetAllCountries();
    }
    
    [HttpGet("get_countries_startswith/{startswith}")]
    public IEnumerable<string> GetCountriesStartsWith(string startswith)
    {
        return _placesService.GetCountries(startswith);
    }
    
    //[Authorize(Roles = $"Driver,Shipper")]
    [HttpGet("get_all_cities")]
    public IEnumerable<string> GetCities()
    {
        return _placesService.GetAllCities();
    }
    
    [HttpGet("get_cities_bycountry/{country}")]
    public IEnumerable<string> GetCities(string country)
    {
        return _placesService.GetCitiesByCountry(country);
    }
    
    [HttpGet("get_cities_startswith/{startswith}")]
    public IEnumerable<string> GetCitiesStartsWith(string startswith)
    {
        return _placesService.GetCities(startswith);
    }
    
    [HttpGet("get_cities_bycountry_startswith/{country}/{startswith}")]
    public IEnumerable<string> GetCitiesStartsWith(string country, string startswith)
    {
        return _placesService.GetCitiesByCountry(country, startswith);
    }
}