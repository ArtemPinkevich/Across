using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Dto;

public class Correlation
{
    public ProfileDto Shipper { set; get; }
    public TruckDto Truck { set; get; }
    public TransportationOrderDto TransportationOrder { set; get; }
}