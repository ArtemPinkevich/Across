using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class GetTransportationLocationCommandHandler : IRequestHandler<GetTransportationLocationCommand, TransportationOrderLocationResultDto>
{
    private readonly IRepository<Transportation> _transportationsRepository;

    public GetTransportationLocationCommandHandler(IRepository<Entities.TransportationOrder> ordersRepository, IRepository<Transportation> transportationsRepository)
    {
        _transportationsRepository = transportationsRepository;
    }
    
    public async Task<TransportationOrderLocationResultDto> Handle(GetTransportationLocationCommand request, CancellationToken cancellationToken)
    {
        var transportation = await _transportationsRepository.GetAsync(x => x.TransportationOrderId == request.TransportationOrderId);
        if (transportation == null)
        {
            return new TransportationOrderLocationResultDto()
            {
                Result = ApiResult.Failed,
                Reasons = new[] { $"no transportaion found for order with id:{request.TransportationOrderId}" }
            };
        }

        if (transportation.RoutePoints.Count <= 0)
        {
            return new TransportationOrderLocationResultDto()
            {
                Result = ApiResult.Failed,
                Reasons = new []{"no route points was added"}
            };
        }

        var routePoint = transportation.RoutePoints.Last();
        return new TransportationOrderLocationResultDto()
        {
            Result = ApiResult.Success,
            Latitude = routePoint.Latitude,
            Longitude = routePoint.Longitude,
        };
    }
}