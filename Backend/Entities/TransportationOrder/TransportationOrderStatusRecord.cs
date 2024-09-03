using System;

namespace Entities;

public class TransportationOrderStatusRecord : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public DateTime ChangeDatetime { set; get; }
    
    public TransportationOrderStatus TransportationOrderStatus { set; get; } 
}