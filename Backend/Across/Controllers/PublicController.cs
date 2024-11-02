using System;
using System.IO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Across.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublicController : ControllerBase
{
    private readonly IMediator _mediator;

    public PublicController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("getTruckMarkerIcon")]
    public IActionResult GetTruckMarkerIcon()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "truck-map-marker.png");

        if (!System.IO.File.Exists(path))
        {
            return NotFound();
        }

        return PhysicalFile(@$"{path}", $"image/png");
    }
}
