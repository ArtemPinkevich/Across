using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class GetTransportationRouteCommandHandler : IRequestHandler<GetTransportationRouteCommand, TransportationOrderRouteResultDto>
{
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Transportation> _transportationsRepository;

    public GetTransportationRouteCommandHandler(IRepository<Entities.TransportationOrder> ordersRepository, IRepository<Transportation> transportationsRepository)
    {
        _ordersRepository = ordersRepository;
        _transportationsRepository = transportationsRepository;
    }
    
    public async Task<TransportationOrderRouteResultDto> Handle(GetTransportationRouteCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetAsync(x => x.Id == request.TransportationOrderId);
        if (order == null)
        {
            return new TransportationOrderRouteResultDto()
            {
                Result = ApiResult.Failed,
                Reasons = new[] { $"no order found with id:{request.TransportationOrderId}" }
            };
        }
        
        var transportation = await _transportationsRepository.GetAsync(x => x.TransportationOrderId == request.TransportationOrderId);
        if (transportation == null)
        {
            return new TransportationOrderRouteResultDto()
            {
                Result = ApiResult.Failed,
                Reasons = new[] { $"no transportaion found for order with id:{request.TransportationOrderId}" }
            };
        }

        if (transportation.RoutePoints == null || transportation.RoutePoints.Count <= 0)
        {
            return new TransportationOrderRouteResultDto()
            {
                Result = ApiResult.Failed,
                Reasons = new []{"no route points was added"}
            };
        }

        return new TransportationOrderRouteResultDto()
        {
            Result = ApiResult.Success,
            DeparturePoint = new RoutePointDto()
            {
                Latitude = order.TransportationInfo.LoadingLatitude,
                Longitude = order.TransportationInfo.LoadingLongitude
            },
            DestinationPoint = new RoutePointDto()
            {
                Latitude = order.TransportationInfo.UnloadingLatitude,
                Longitude = order.TransportationInfo.UnloadingLongitude
            },
            RoutePoints = transportation.RoutePoints.Select(x => new RoutePointDto()
            {
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                UpdatedDateTime = x.DateTime
            }).ToList()
        };
    }
}