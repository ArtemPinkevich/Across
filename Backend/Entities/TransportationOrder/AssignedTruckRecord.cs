using System;

namespace Entities;

public class AssignedTruckRecord : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public int TruckId { set; get; }
    public Truck Truck { set; get; }
    
    public DateTime ChangeDatetime { set; get; }
}