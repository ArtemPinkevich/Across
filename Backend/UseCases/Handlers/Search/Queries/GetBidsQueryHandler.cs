using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Queries;

public class GetBidsQueryHandler : IRequestHandler<GetBidsQuery, BidsResultDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _transportationOrderRepository;
    private readonly IRepository<Entities.Truck> _trucksRepository;

    public GetBidsQueryHandler(IMapper mapper,
        UserManager<User> userManager,
        IRepository<Entities.Truck> trucksRepository,
        IRepository<Entities.TransportationOrder> transportationOrderRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _trucksRepository = trucksRepository;
        _transportationOrderRepository = transportationOrderRepository;
    }
    
    public async Task<BidsResultDto> Handle(GetBidsQuery request, CancellationToken cancellationToken)
    {
        var bidsResultDto = new BidsResultDto { Correlations = new List<CorrelationDto>() };

        var orders = await _transportationOrderRepository.GetFirstAsync(x => x.DriverRequests.Count > 0, 50);

        foreach (var order in orders)
        {
            foreach (var driverRequest in order.DriverRequests.Where(o => o.Status == DriverRequestStatus.PendingLogistReview))
            {
                bidsResultDto.Correlations.Add(await CreateCorrelation(order, driverRequest));
            }
        }

        return bidsResultDto;
    }

    private async Task<CorrelationDto> CreateCorrelation(Entities.TransportationOrder order, Entities.DriverRequest driverRequest)
    {
        var shipper = await _userManager.FindByIdWithDocuments(order.ShipperId);
        var driver = await _userManager.FindByIdWithDocuments(driverRequest.DriverId);
        var truck = await _trucksRepository.GetAsync(x => x.Id == driverRequest.TruckId);
        var correlation = new CorrelationDto {
            Shipper = await shipper.ConvertToProfileDto(_userManager, _mapper),
            Driver = await driver.ConvertToProfileDto(_userManager, _mapper),
            Truck = _mapper.Map<TruckDto>(truck),
            TransportationOrder = _mapper.Map<TransportationOrderDto>(order)
        };

        return correlation;
    }
}