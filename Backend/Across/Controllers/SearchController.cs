using System;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Search.Queries;

namespace Across.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class SearchController: ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Admin}")]
    [HttpGet("search")]
    public async Task<SearchResultDto> Search([FromQuery] SearchQuery searchDto)
    {
        var result = await _mediator.Send(searchDto);

        return result;
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("search_drivers")]
    public async Task<SearchDriversResultDto> SearchDrivers([FromQuery] SearchDriversQuery searchDto)
    {
        return await _mediator.Send(searchDto);
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("search_driver_by_truck_id/{truck_id}")]
    public async Task<ProfileDto> SearchDriverByTruckId(int truck_id)
    {
        return await _mediator.Send(new SearchDriverByTruckIdQuery(){ TruckId = truck_id });
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("search_shipper_by_order_id/{order_id}")]
    public async Task<ProfileDto> SearchShipperByOrderId(int order_id)
    {
        return await _mediator.Send(new SearchShipperByOrderIdQuery(){ OrderId = order_id });
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("search_order_by_id/{id}")]
    public async Task<TransportationOrdersListDto> SearchTransportationOrderById(int id)
    {
        return await _mediator.Send(new SearchOrderByIdQuery(){ OrderId = id });
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("get_bids")]
    public async Task<SearchResultDto> GetBids()
    {
        return null;
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("get_recommendations")]
    public async Task<SearchResultDto> GetRecommendations()
    {
        return null;
    }
}
