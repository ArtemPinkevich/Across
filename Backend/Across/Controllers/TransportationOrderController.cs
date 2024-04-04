using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Cargo.Queries;
using UseCases.Handlers.TransportationOrder.Commands;

namespace Across.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class TransportationOrderController:ControllerBase
{
    private readonly IMediator _mediator;
    
    public TransportationOrderController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Roles = $"{UserRoles.Shipper}")]
    [HttpPost("get_orders")]
    public async Task<TransportationOrdersListDto> GetLoads()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetTransportationOrdersQuery()
        {
            UserId = userId
        });
    }

    [Authorize(Roles = UserRoles.Shipper)]
    [HttpPost("add_or_update_order")]
    public async Task<TransportationOrderResult> AddOrUpdateLoad([FromBody] TransportationOrderDto transportationOrder)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new AddOrUpdateTransportationOrderCommand()
        {
            UserId = userId,
            TransportationOrderDto = transportationOrder
        });
    }
    
    [Authorize(Roles = UserRoles.Shipper)]
    [HttpPost("delete_load")]
    public async Task<TransportationOrderResult> DeleteLoad([FromBody] DeleteTransportationOrderCommand command)
    {
        return await _mediator.Send(command);
    }
}