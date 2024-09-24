namespace UseCases.Handlers.Cargo.Dto;

public class TransportationOrderLocationResultDto : TransportationOrderResult
{
    public string Latitude { set; get; }
    public string Longitude { set; get; }
    public string UpdatedDateTime { set; get; }
}