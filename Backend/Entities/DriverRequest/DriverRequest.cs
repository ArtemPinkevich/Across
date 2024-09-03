namespace Entities;

public class DriverRequest : EntityBase
{
    public string DriverId { set; get; }
    public Driver Driver { set; get; }
    
    public int TruckId { set; get; }
    public Truck Truck { set; get; }
    
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public DriverRequestStatus Status { set; get; }
    
    public string CreatedDateTime { set; get; }
}