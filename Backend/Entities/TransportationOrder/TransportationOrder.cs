using System.Collections.Generic;

namespace Entities;

public class TransportationOrder : EntityBase
{
    public string ShipperId { set; get; }
    public User Shipper { set; get; }
    
    public Cargo Cargo { set; get; }
    
    public TruckRequirements TruckRequirements { set; get; }
    
    public int Price { set; get; }
    
    public string LoadDateFrom { set; get; }
    
    public string LoadDateTo { set; get; }
    
    public string LoadingLocalityName { set; get; }
    
    public string LoadingAddress { set; get; }
    
    public string UnloadingLocalityName { set; get; }
    
    public string UnloadingAddress { set; get; }
    
    public TransportationOrderStatus TransportationOrderStatus { set; get; }
    
    /// <summary>
    /// List of drivers who want to take this order
    /// </summary>
    public List<DriverRequest> DriverRequests { set; get; }
}