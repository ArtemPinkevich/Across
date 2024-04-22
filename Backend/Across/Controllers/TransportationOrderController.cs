﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.TransportationOrder.Commands;
using UseCases.Handlers.TransportationOrder.Queries;

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
    [HttpGet("get_orders")]
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
    [HttpDelete("delete_load/{id}")]
    public async Task<TransportationOrderResult> DeleteLoad(int id)
    {
        var command = new DeleteTransportationOrderCommand()
        {
            TransportationOrderId = id,
        };

        return await _mediator.Send(command);
    }
}