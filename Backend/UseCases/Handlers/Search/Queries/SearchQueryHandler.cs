using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Search.Dto;
using System;
using System.Collections.Generic;
using Entities;
using UseCases.Handlers.TransportationOrder.Mapping;

namespace UseCases.Handlers.Search.Queries;

public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchResultDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _repository;

    public SearchQueryHandler(IRepository<Entities.TransportationOrder> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SearchResultDto> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var result = new SearchResultDto();

        // Ожидаем в request.LoadDate дату в ISO формате
        if (!DateTime.TryParse(request.LoadingDate, out DateTime desiredLoadDate))
        {
            result.Result = ApiResult.Failed;
            return result;
        }

        var transportationOrders = 
            await _repository.GetAllAsync(x => x.TransportationOrderStatus == TransportationOrderStatus.CarrierFinding);

        var fromAddressLower = request.FromAddress?.ToLower();
        var toAddressLower = request.ToAddress?.ToLower();

        var filtredOrders = new List<Entities.TransportationOrder>();

        transportationOrders.ForEach(order =>
        {
            var loadingPlace = TransportationOrderMapperProfile.ConvertLocationReverse(order.TransportationInfo.LoadingLocalityName, order.TransportationInfo.LoadingLatitude, order.TransportationInfo.LoadingLongitude);
            if (!loadingPlace.City.ToLower().Contains(fromAddressLower))
            {
                return;
            }

            if (!string.IsNullOrEmpty(toAddressLower) && toAddressLower != order.TransportationInfo.UnloadingAddress.ToLower())
            {
                var unloadingPlace = TransportationOrderMapperProfile.ConvertLocationReverse(order.TransportationInfo.UnloadingLocalityName, order.TransportationInfo.UnloadingLatitude, order.TransportationInfo.UnloadingLongitude);
                if (!unloadingPlace.City.ToLower().Contains(toAddressLower))
                {
                    return;
                }
            }

            // Ожидаем в order.LoadDateFrom дату в ISO формате
            if (!DateTime.TryParse(order.TransportationInfo.LoadDateFrom, out DateTime loadDateFrom))
            {
                return;
            }

            
            filtredOrders.Add(order);
            return;
            
            if (desiredLoadDate.Date == loadDateFrom.Date)
            {
                filtredOrders.Add(order);
                return;
            }

            if (DateTime.TryParse(order.TransportationInfo.LoadDateTo, out DateTime loadDateTo))
            {
                if (desiredLoadDate.Date >= loadDateFrom.Date && desiredLoadDate.Date <= loadDateTo.Date)
                {
                    filtredOrders.Add(order);
                }
                return;
            }
            else
            {
                filtredOrders.Add(order);
            }
        });

        return new SearchResultDto()
        {
            Result = ApiResult.Success,
            TransportationOrders = filtredOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToList()
        };
    }
}
