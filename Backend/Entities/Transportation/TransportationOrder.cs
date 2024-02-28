namespace Entities;

public class TransportationOrder
{
    public Cargo Cargo { set; get; }
    
    public TruckRequirements TruckRequirements { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; }
    
    public string LoadDateFrom { set; get; }
    
    public string LoadDateTo { set; get; }
    
    public string LoadingLocalityName { set; get; }
    
    public string LoadingAddress { set; get; }
    
    public string UnloadingLocalityName { set; get; }
    
    public string UnloadingAddress { set; get; }
}