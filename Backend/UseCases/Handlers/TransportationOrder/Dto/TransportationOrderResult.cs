namespace UseCases.Handlers.Cargo.Dto;

public enum Result
{
    Ok,
    Error
}
public class TransportationOrderResult
{
    public Result Result { set; get; }
    public string[] Reasons { set; get; }
}