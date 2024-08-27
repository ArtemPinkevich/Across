using System.Collections.Generic;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Search.Dto;

public class OrdersInProgressResultDto
{
    public ApiResult Result { set; get; }
    public string[] Reasons { set; get; }
    public List<CorrelationDto> OrdersInProgress { set; get; }
}