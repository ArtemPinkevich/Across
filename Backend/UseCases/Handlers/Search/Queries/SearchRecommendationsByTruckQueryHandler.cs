using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Search.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchRecommendationsByTruckQueryHandler : IRequestHandler<SearchRecommendationsByTruckQuery, SearchResultDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.Truck> _trucksRepository;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;

    public SearchRecommendationsByTruckQueryHandler(IMapper mapper,
        IRepository<Entities.Truck> trucksRepository,
        IRepository<Entities.TransportationOrder> ordersRepository)
    {
        _mapper = mapper;
        _trucksRepository = trucksRepository;
        _ordersRepository = ordersRepository;
    }
    
    public async Task<SearchResultDto> Handle(SearchRecommendationsByTruckQuery request, CancellationToken cancellationToken)
    {
        var truck = await _trucksRepository.GetAsync(x => x.Id == request.TruckId);
        if (truck == null)
        {
            return new SearchResultDto()
            {
                Result = ApiResult.Failed, Reasons = new[] { $"no trucks found with id {request.TruckId}" }
            };
        }

        var orders =
            await _ordersRepository.GetAllAsync(x => x.LoadingLocalityName == truck.TruckLocation
                                                     && x.TruckRequirements.LoadingType == truck.LoadingType
                                                     && x.TruckRequirements.InnerBodyHeight >= truck.InnerBodyHeight
                                                     && x.TruckRequirements.CarryingCapacity >= truck.CarryingCapacity
                                                     && x.TruckRequirements.InnerBodyLength >= truck.InnerBodyLength
                                                     && x.TruckRequirements.InnerBodyWidth >= truck.InnerBodyWidth);

        return new SearchResultDto()
        {
            Result = ApiResult.Success,
            TransportationOrders = orders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToList()
        };
    }
}