namespace Entities;

public class RoutePoint: EntityBase
{
    public int TransportationId { set; get; }
    public Transportation Transportation { set; get; }
    
    public string LocationName { set; get; }
    
    public string Latitude { set; get; }
    
    public string Longtitude { set; get; }
}