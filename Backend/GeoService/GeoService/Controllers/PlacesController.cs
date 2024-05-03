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

    public PlacesController(ILogger<PlacesController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpGet("get_countries")]
    public IEnumerable<string> GetCountries()
    {
        List<string> countries = new List<string>()
        {
            "Cameroon",
            "Austria",
            "Finland"
        };
        return countries;
    }
    
    [Authorize]
    [HttpGet("get_cities")]
    public IEnumerable<string> GetCities()
    {
        List<string> countries = new List<string>()
        {
            "CityName1",
            "CityName2",
            "CityName3"
        };
        return countries;
    }
}