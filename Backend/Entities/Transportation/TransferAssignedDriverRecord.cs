using System;

namespace Entities;

public class TransferAssignedDriverRecord : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public string UserId { set; get; }
    public User User { set; get; }
    
    public DateTime ChangeDatetime { set; get; }
}