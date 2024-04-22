using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Cargo.Dto;

public class TransportationOrderResult
{
    public int? TransportationId { set; get; }
    public ApiResult Result { set; get; }
    public string[] Reasons { set; get; }
}