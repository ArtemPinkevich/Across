using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Truck.Commands;
using UseCases.Handlers.Truck.Dto;

namespace Across.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class TruckController: ControllerBase
{
    private readonly IMediator _mediator;

    public TruckController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Roles = UserRoles.Driver)]
    [HttpPost("add_truck")]
    public async Task<TruckResultDto> AddTruck([FromBody] TruckDto addTruckToUserDto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new AddTruckToUserCommand()
        {
            UserId = userId,
            TruckDto = addTruckToUserDto
        });
    }

    [Authorize(Roles = UserRoles.Driver)]
    [HttpPost("update_truck")]
    public async Task<TruckResultDto> UpdateTruck()
    {
        return new TruckResultDto() { };
    }
    
    [Authorize(Roles = UserRoles.Driver)]
    [HttpPost("delete_truck")]
    public async Task<TruckResultDto> DeleteTruck()
    {
        return new TruckResultDto() { };
    }
}