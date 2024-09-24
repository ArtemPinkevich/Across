using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
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
    private readonly IRepository<DriverRequest> _driverRequestRepository;

    public SearchRecommendationsByTruckQueryHandler(IMapper mapper,
        IRepository<Entities.Truck> trucksRepository,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<DriverRequest> driverRequestRepository)
    {
        _mapper = mapper;
        _trucksRepository = trucksRepository;
        _ordersRepository = ordersRepository;
        _driverRequestRepository = driverRequestRepository;
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
            await _ordersRepository.GetAllAsync(x => x.TransportationInfo.LoadingLocalityName == truck.TruckLocation
                                                     && x.TruckRequirements.LoadingType == truck.LoadingType
                                                     && x.TruckRequirements.InnerBodyHeight >= truck.InnerBodyHeight
                                                     && x.TruckRequirements.CarryingCapacity >= truck.CarryingCapacity
                                                     && x.TruckRequirements.InnerBodyLength >= truck.InnerBodyLength
                                                     && x.TruckRequirements.InnerBodyWidth >= truck.InnerBodyWidth);

        var driverRequests = await _driverRequestRepository.GetAllAsync(o => o.TruckId == request.TruckId);
        var filtredOrders = orders.Where(o => !driverRequests.Any(r => r.TransportationOrderId == o.Id));


        return new SearchResultDto()
        {
            Result = ApiResult.Success,
            TransportationOrders = filtredOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToList()
        };
    }
}