using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class DeleteTruckCommand: IRequest<TruckResultDto>
{
    public int TruckId { set; get; }
}