using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Cargo.Commands;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Cargo.Queries;

namespace Across.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class LoadController:ControllerBase
{
    private readonly IMediator _mediator;
    
    public LoadController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Roles = $"{UserRoles.Shipper}")]
    [HttpPost("get_loads")]
    public async Task<CargosListDto> GetLoads()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetCargosQuery()
        {
            UserId = userId
        });
    }

    [Authorize(Roles = UserRoles.Shipper)]
    [HttpPost("add_or_update_load")]
    public async Task<CargoResult> AddOrUpdateLoad([FromBody] CargoDto cargo)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new AddOrUpdateCargoCommand()
        {
            UserId = userId,
            CargoDto = cargo
        });
    }
    
    [Authorize(Roles = UserRoles.Shipper)]
    [HttpPost("delete_load")]
    public async Task<CargoResult> DeleteLoad([FromBody] DeleteCargoCommand command)
    {
        return await _mediator.Send(command);
    }
}