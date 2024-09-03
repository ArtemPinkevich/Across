using System;

namespace Entities;

public class TransportationStatusRecord : EntityBase
{
    public int TransportationId { set; get; }
    public Transportation TransportationOrder { set; get; }
    
    public DateTime ChangeDatetime { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; } 
}