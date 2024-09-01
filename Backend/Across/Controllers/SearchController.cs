using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
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
    [HttpGet("search_trucks")]
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
    [HttpGet("get_bids")]
    public async Task<BidsResultDto> GetBids()
    {
        return await _mediator.Send(new GetBidsQuery());
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("search_recommendations")]
    public async Task<RecommendationsResultDto> SearchRecommendations()
    {
        return await _mediator.Send(new SearchRecommendationsQuery());
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Admin}")]
    [HttpGet("search_recommendations_by_truck/{truck_id}")]
    public async Task<SearchResultDto> SearchRecommendationsByTruck(int truck_id)
    {
        return await _mediator.Send(new SearchRecommendationsByTruckQuery() { TruckId = truck_id });
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("search_orders_in_progress")]
    public async Task<OrdersInProgressResultDto> SearchOrdersInProgress()
    {
        return await _mediator.Send(new GetOrdersInProgressQuery());
    }

    [Authorize(Roles = UserRoles.Shipper)]
    [HttpGet("search_orders_in_shipper_approving")]
    public async Task<OrdersInProgressResultDto> SearchOrdersInShipperApproving()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimsTypes.Id)?.Value;

        return await _mediator.Send(new SearchOrdersInShipperApprovingQuery() { UserId = userId });
    }
}
