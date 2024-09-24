using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class SetTruckLocationCommand:IRequest<TruckResultDto>
{
    public int TruckId { set; get; }
    
    public string TruckLocation { set; get; }
    
    public string Latitude { set; get; }
    
    public string Longitude { set; get; }
}