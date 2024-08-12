using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Dto;

public class DriverDto
{
    public string UserId { set; get; }
    
    public string Name { set; get; }
    
    public string Surname { set; get; }
    
    public TruckDto Truck { set; get; }
}