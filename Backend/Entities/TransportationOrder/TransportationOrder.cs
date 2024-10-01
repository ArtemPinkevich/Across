using System.Collections.Generic;

namespace Entities;

public class TransportationOrder : EntityBase
{
    public string ShipperId { set; get; }
    public User Shipper { set; get; }
    
    public Cargo Cargo { set; get; }
    
    public TruckRequirements TruckRequirements { set; get; }
    
    public ContactInformation ContactInformation { set; get; }
    
    public TransportationInfo TransportationInfo { set; get; }
    
    public int Price { set; get; }
    
    public TransportationOrderStatus TransportationOrderStatus { set; get; }
    
    /// <summary>
    /// List of drivers who want to take this order
    /// </summary>
    public List<DriverRequest> DriverRequests { set; get; }
}