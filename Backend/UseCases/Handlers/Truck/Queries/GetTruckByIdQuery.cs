using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Queries;

public class GetTruckByIdQuery : IRequest<TruckDto>
{
    public int TruckId { set; get; }
}