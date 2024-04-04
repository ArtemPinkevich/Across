namespace UseCases.Handlers.Cargo.Dto;

public enum Result
{
    Ok,
    Error
}
public class TransportationOrderResult
{
    public int? TransportationId { set; get; }
    public Result Result { set; get; }
    public string[] Reasons { set; get; }
}