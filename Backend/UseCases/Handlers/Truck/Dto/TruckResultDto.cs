using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Truck.Dto;

public class TruckResultDto
{
    public ApiResult Result { set; get; }
    
    public string[] Reasons { set; get; }
}