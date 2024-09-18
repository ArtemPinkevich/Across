using GeoService.Dto;
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
   
    [HttpGet("get_places/{startswith}")]
    public async Task<IEnumerable<PlaceDto>> GetCountriesStartsWith(string startswith)
    {
        return await _placesService.GetPlaces(startswith);
    }
}