namespace UseCases.Handlers.Cargo.Dto;

public enum Result
{
    Ok,
    Error
}
public class CargoResult
{
    public Result Result { set; get; }
    public string[] Reasons { set; get; }
}