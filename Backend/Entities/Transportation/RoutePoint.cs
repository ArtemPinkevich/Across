namespace Entities;

public class RoutePoint: EntityBase
{
    public int TransportationId { set; get; }
    public Transportation Transportation { set; get; }
    
    public string LocationName { set; get; }
    
    public string Latitude { set; get; }
    
    public string Longitude { set; get; }
    
    public string DateTime { set; get; }
}