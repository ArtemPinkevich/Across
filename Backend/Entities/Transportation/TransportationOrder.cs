using System.Collections.Generic;

namespace Entities;

public class TransportationOrder : EntityBase
{
    public string UserId { set; get; }
    public User User { set; get; }
    
    public Cargo Cargo { set; get; }
    
    public TruckRequirements TruckRequirements { set; get; }
    
    public List<TransferAssignedTruckRecord> AssignedTruckRecords { set; get; }
    
    public List<TransferChangeStatusRecord> TransferChangeHistoryRecords { set; get; }
    
    public string LoadDateFrom { set; get; }
    
    public string LoadDateTo { set; get; }
    
    public string LoadingLocalityName { set; get; }
    
    public string LoadingAddress { set; get; }
    
    public string UnloadingLocalityName { set; get; }
    
    public string UnloadingAddress { set; get; }
    
    /// <summary>
    /// List of trucks who want to take this order
    /// </summary>
    public List<Truck> Trucks { set; get; }
}