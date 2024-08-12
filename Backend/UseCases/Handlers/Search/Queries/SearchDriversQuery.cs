using Entities;
using MediatR;
using UseCases.Handlers.Search.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchDriversQuery: IRequest<SearchDriversResultDto>
{
    public TrailerType? TrailerType { set; get; }
    
    public CarBodyType? CarBodyType { set; get; }
    
    public LoadingType? LoadingType { set; get; }
    
    public int? CarryingCapacity { set; get; }
    
    public int? BodyVolume { set; get; }
    
    public int? InnerBodyLength { set; get; }
    
    public int? InnerBodyWidth { set; get; }
    
    public int? InnerBodyHeight { set; get; }
}