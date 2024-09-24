namespace Entities;

public class TransportationInfo : EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public string LoadDateFrom { set; get; }
    
    public string LoadDateTo { set; get; }
    
    public string LoadingLocalityName { set; get; }
    
    public string LoadingAddress { set; get; }
    
    public string LoadingLatitude { set; get; }
    
    public string LoadingLongitude { set; get; }
    
    public string UnloadingLocalityName { set; get; }
    
    public string UnloadingAddress { set; get; }
    
    public string UnloadingLatitude { set; get; }
    
    public string UnloadingLongitude { set; get; }
}