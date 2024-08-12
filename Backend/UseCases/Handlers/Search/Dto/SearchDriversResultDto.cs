using System.Collections.Generic;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Search.Dto;

public class SearchDriversResultDto
{
    public ApiResult Result { set; get; }

    public string[] Reasons { set; get; }
    
    public List<DriverDto> Drivers { set; get; }
}