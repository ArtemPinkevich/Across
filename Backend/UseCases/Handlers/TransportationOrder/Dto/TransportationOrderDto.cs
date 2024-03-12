using Entities;

namespace UseCases.Handlers.Cargo.Dto;

public class TransportationOrderDto
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
}