using Entities;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Cargo.Dto;

public class LoadPublishInfoDto
{
    public string LoadDateFrom { set; get; }
    
    public string LoadDateTo { set; get; }
    
    public string LoadingLocalityName { set; get; }
    
    public string LoadingAddress { set; get; }
    
    public string UnloadingLocalityName { set; get; }
    
    public string UnloadingAddress { set; get; }
}

public class TruckRequirementsForLoadDto
{
    public CarBodyType[] CarBodies { set; get; }
    public LoadingTypeDto[] LoadingTypeDtos { set; get; }
    public LoadingTypeDto[] UnloadingType { set; get; }
    
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
    
    public bool HasLTtl { set; get; }
    public bool HasLiftGate { set; get; }
    public bool HasStanchionTrailer { set; get; }
    public int CarryingCapacity { set; get; }

}

public class LoadDto
{
    public int? Id { set; get; }
    public string CreatedId { set; get; }
    public string Name { set; get; }
    public int Weight { set; get; }
    public int Volume { set; get; }
    public PackagingType PackagingType { set; get; }
    public int? PackagingQuantity { set; get; }
    public double? Length { set; get; }
    public double? Width { set; get; }
    public double? Height { set; get; }
    public double Diameter { set; get; }
    public TruckRequirementsForLoadDto TruckRequirementsForLoadDto { set; get; }
}

public class TransportationOrderDto
{
    public int? TransportationOrderId { set; get; }
    
    public LoadPublishInfoDto LoadPublishInfo { set; get; }
    
    public LoadDto Load { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; }
}