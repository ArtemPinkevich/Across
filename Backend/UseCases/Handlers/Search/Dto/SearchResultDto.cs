using System.Collections.Generic;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Search.Dto;

public class SearchResultDto
{
    public ApiResult Result { set; get; }

    public string[] Reasons { set; get; }

    public List<TransportationOrderDto> TransportationOrders { set; get; }
}
