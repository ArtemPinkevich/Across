using System.Collections.Generic;

namespace UseCases.Handlers.Cargo.Dto;

public class TransportationOrdersListDto
{
    public TransportationOrderResult Result { set; get; }
    public List<TransportationOrderDto> TransportationOrderDtos { set; get; }
}