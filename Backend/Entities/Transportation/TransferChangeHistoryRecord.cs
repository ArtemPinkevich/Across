using System;

namespace Entities;

public class TransferChangeHistoryRecord : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public string AssignedDriverId { set; get; }
    public User AssignedDriver { set; get; }
    
    public string AssignedManagerId { set; get; }
    public User AssignedManager { set; get; }
    
    public string AssignedLawyerId { set; get; }
    public User AssignedLawyer { set; get; }
    
    public DateTime ChangeDatetime { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; } 
}