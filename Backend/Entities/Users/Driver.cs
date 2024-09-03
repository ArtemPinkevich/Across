using System.Collections.Generic;

namespace Entities;

public class Driver : User
{
    /// <summary>
    /// List of Trucks of Driver
    /// </summary>
    public List<Truck> Trucks { set; get; }
    
    public List<DriverRequest> DriverRequests { set; get; }
}