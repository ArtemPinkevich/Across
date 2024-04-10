using System;
using System.Collections.Generic;
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
using UseCases.Handlers.Truck.Queries;

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
    [HttpGet("get_trucks")]
    public async Task<TrucksListResultDto> GetTrucks()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetTrucksQuery()
        {
            UserId = userId,
        });
    }

    [Authorize(Roles = UserRoles.Driver)]
    [HttpPost("add_or_update_truck")]
    public async Task<TruckResultDto> AddTruck([FromBody] TruckDto addTruckToUserDto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new AddOrUpdateTruckToUserCommand()
        {
            UserId = userId,
            TruckDto = addTruckToUserDto
        });
    }
    
    [Authorize(Roles = UserRoles.Driver)]
    [HttpDelete("delete_truck/{id}")]
    public async Task<TruckResultDto> DeleteTruck(int id)
    {
        var deleteTruckCommand = new DeleteTruckCommand()
        {
            TruckId = id,
        };

        return await _mediator.Send(deleteTruckCommand);
    }
}