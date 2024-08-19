using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class SetTruckLocationCommand:IRequest<TruckResultDto>
{
    public int TruckId { set; get; }
    
    public string TruckLocation { set; get; }
}