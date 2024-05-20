using System;

namespace Entities;

public class TransferChangeStatusRecord : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public DateTime ChangeDatetime { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; } 
}