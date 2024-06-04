using Entities;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Cargo.Dto;

public class LocationDto
{
    public string Country { set; get; }
    
    public string Region { set; get; }
    
    public string City { set; get; }
}

public class TransferInfoDto
{
    public string LoadingDateFrom { set; get; }
    
    public string LoadingDateTo { set; get; }
    
    public LocationDto LoadingLocation { set; get; }
    
    public string LoadingAddress { set; get; }
    
    public LocationDto UnloadingLocation { set; get; }
    
    public string UnloadingAddress { set; get; }
}

public class TruckRequirementsDto
{
    public CarBodyType[] CarBodies { set; get; }
    public LoadingTypeDto[] LoadingTypeDtos { set; get; }
    public LoadingTypeDto[] UnloadingTypeDtos { set; get; }
    
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
    
    public bool HasLtl { set; get; }
    public bool HasLiftgate { set; get; }
    public bool HasStanchionTrailer { set; get; }
    public int CarryingCapacity { set; get; }
}

public class CargoDto
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
    public TruckRequirementsDto TruckRequirements { set; get; }
}

public class TransportationOrderDto
{
    public int? TransportationOrderId { set; get; }
    
    public TransferInfoDto TransferInfo { set; get; }
    
    public CargoDto Cargo { set; get; }
    
    public TransportationStatus TransportationStatus { set; get; }
}