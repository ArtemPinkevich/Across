using Entities;

namespace UseCases.Handlers.Truck.Dto;

public class TruckDto
{
    public string createdId { set; get; }
    
    public string regNumber { set; get; }
    
    public TrailerType TrailerType { set; get; }
    
    public CarBodyType CarBodyType { set; get; }
    
    public LoadingType LoadingType { set; get; }
    
    public bool HasLTtl { set; get; }
    
    public bool HasLiftGate { set; get; }
    
    public bool HasStanchionTrailer { set; get; }
    
    public int BodyVolume { set; get; }
    
    public int InnerBodyLength { set; get; }
    
    public int InnerBodyWidth { set; get; }
    
    public int InnerBodyHeight { set; get; }
    
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