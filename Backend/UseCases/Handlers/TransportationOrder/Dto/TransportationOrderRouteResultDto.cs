using System.Collections.Generic;

namespace UseCases.Handlers.Cargo.Dto;

public class RoutePointDto
{
    public string Latitude { set; get; }
    public string Longitude { set; get; }
    public string UpdatedDateTime { set; get; }
}

public class TransportationOrderRouteResultDto : TransportationOrderResult
{
    public RoutePointDto DeparturePoint { set; get; }
    public RoutePointDto DestinationPoint { set; get; }
    public List<RoutePointDto> RoutePoints { set; get; }
}