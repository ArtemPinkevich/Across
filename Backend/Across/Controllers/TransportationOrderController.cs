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
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Search.Queries;
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

    [Authorize(Roles = $"{UserRoles.Shipper},{UserRoles.Owner}")]
    [HttpGet("get_orders")]
    public async Task<TransportationOrdersListDto> GetShipperTransportationOrders()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetShipperOrdersQuery()
        {
            UserId = userId
        });
    }
    
    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Owner}")]
    [HttpGet("get_requested_orders")]
    public async Task<TransportationOrdersListDto> GetDriverRequestedTransportationOrders()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetDriverRequestedOrdersQuery()
        {
            UserId = userId
        });
    } 
    
    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Owner}")]
    [HttpGet("get_assigned_orders")]
    public async Task<TransportationOrdersListDto> GetDriverTransportationOrders()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetDriverAssignedOrdersQuery()
            {
                UserId = userId
            });
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Shipper}")]
    [HttpGet("get_orders_history")]
    public async Task<TransportationOrdersListDto> GetOrdersHistory()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetOrdersHistoryQuery()
        {
            UserId = userId
        });
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("get_orders_in_progress")]
    public async Task<OrdersInProgressResultDto> SearchOrdersInProgress()
    {
        return await _mediator.Send(new GetOrdersInProgressQuery());
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("get_bids")]
    public async Task<BidsResultDto> GetBids()
    {
        return await _mediator.Send(new GetBidsQuery());
    }

    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Owner}")]
    [HttpGet("get_order_by_id/{id}")]
    public async Task<TransportationOrdersListDto> GetTransportationOrderById(int id)
    {
        return await _mediator.Send(new GetOrderByIdQuery() { OrderId = id });
    }

    [Authorize(Roles = $"{UserRoles.Shipper},{UserRoles.Owner}")]
    [HttpPost("add_or_update_order")]
    public async Task<TransportationOrderResult> AddOrUpdateOrder([FromBody] TransportationOrderDto transportationOrder)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new AddOrUpdateTransportationOrderCommand()
        {
            UserId = userId,
            TransportationOrderDto = transportationOrder
        });
    }

    [Authorize(Roles = $"{UserRoles.Shipper},{UserRoles.Owner}")]
    [HttpDelete("delete_load/{id}")]
    public async Task<TransportationOrderResult> DeleteLoad(int id)
    {
        var command = new DeleteTransportationOrderCommand()
        {
            TransportationOrderId = id,
        };

        return await _mediator.Send(command);
    }
    
    [Authorize(Roles = UserRoles.Driver)]
    [HttpPost("try_take_order")]
    public async Task<TransportationOrderResult> TryTakeOrder([FromBody] TryTakeOrderCommand tryTakeOrderCommand)
    {
        return await _mediator.Send(tryTakeOrderCommand);
    }

    [Authorize(Roles = $"{UserRoles.Shipper},{UserRoles.Admin}")]
    [HttpPost("assign_truck")]
    public async Task<TransportationOrderResult> AssignTruck([FromBody] AssignTruckCommand assignTruckCommand)
    {
        return await _mediator.Send(assignTruckCommand);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("start_shipper_approving")]
    public async Task<TransportationOrderResult> StartShipperApproving([FromBody] StartShipperApprovingCommand command)
    {
        return await _mediator.Send(command);
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Admin}")]
    [HttpPost("inform_arrival_for_loading/{id}")]
    public async Task<TransportationOrderResult> InformArrivalForLoading(int id)
    {
        var command = new ChangeTransportationStatusCommand()
        {
            TransportationOrderId = id,
            TransportationStatus = TransportationOrderStatus.Loading,
        };

        return await _mediator.Send(command);
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Admin}")]
    [HttpPost("start_transportation/{id}")]
    public async Task<TransportationOrderResult> StartTransportation(int id)
    {
        var command = new ChangeTransportationStatusCommand()
        {
            TransportationOrderId = id,
            TransportationStatus = TransportationOrderStatus.Transporting,
        };

        return await _mediator.Send(command);
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Admin}")]
    [HttpPost("inform_arrival_for_unloading/{id}")]
    public async Task<TransportationOrderResult> InformArrivalForUnloading(int id)
    {
        var command = new ChangeTransportationStatusCommand()
        {
            TransportationOrderId = id,
            TransportationStatus = TransportationOrderStatus.Unloading,
        };

        return await _mediator.Send(command);
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Admin}")]
    [HttpPost("delivered_transportation/{id}")]
    public async Task<TransportationOrderResult> DeliveredTransportation(int id)
    {
        var command = new ChangeTransportationStatusCommand()
        {
            TransportationOrderId = id,
            TransportationStatus = TransportationOrderStatus.Delivered,
        };

        return await _mediator.Send(command);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("done_transportation/{id}")]
    public async Task<TransportationOrderResult> DoneTransportation(int id)
    {
        var command = new ChangeTransportationStatusCommand()
        {
            TransportationOrderId = id,
            TransportationStatus = TransportationOrderStatus.Done,
        };

        return await _mediator.Send(command);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("clarify")]
    public async Task<TransportationOrderResult> Clarify([FromBody] ClarifyCommand clarifyCommand)
    {
        return await _mediator.Send(clarifyCommand);
    }
}
