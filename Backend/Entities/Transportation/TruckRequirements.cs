namespace Entities;

public class TruckRequirements: EntityBase
{
    public int TransportationOrderId { set; get; }
    public TransportationOrder TransportationOrder { set; get; }
    
    public LoadingType LoadingType { set; get; }
    
    public LoadingType UnloadingType { set; get; }

    public int CarBodyRequirementId { set; get; }
    public CarBodyRequirement CarBodyRequirement{ set; get; }
    
    public bool HasLTtl { set; get; }
    
    public bool HasLiftGate { set; get; }
    
    public bool HasStanchionTrailer { set; get; }
    
    public int CarryingCapacity { set; get; }
    
    public bool Adr1 { set; get; }
    
    public bool Adr2 { set; get; }
    
    public bool Adr3 { set; get; }
    
    public bool Adr4 { set; get; }
    
    public bool Adr5 { set; get; }
    
    public bool Adr6 { set; get; }
    
    public bool Adr7 { set; get; }
    
    public bool Adr8 { set; get; }
    
    public bool Adr9 { set; get; }
    
    public bool Tir { set; get; }
    
    public bool Ekmt { set; get; }
}