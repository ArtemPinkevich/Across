using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class AddTransportationLocationCommandHandler: IRequestHandler<AddTransportationLocationCommand, TransportationOrderResult>
{
    private readonly IRepository<Transportation> _transportationsRepository;

    public AddTransportationLocationCommandHandler(IRepository<Transportation> transportationsRepository)
    {
        _transportationsRepository = transportationsRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(AddTransportationLocationCommand request, CancellationToken cancellationToken)
    {

        var transportation = await _transportationsRepository.GetAsync(x => x.TransportationOrderId == request.TransportationOrderId);
        if (transportation == null)
        {
            return new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new[] { $"no transportaion found for order with id:{request.TransportationOrderId}" }
            };
        }

        transportation.RoutePoints ??= new List<RoutePoint>();
        
        transportation.RoutePoints.Add(new RoutePoint()
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            DateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            TransportationId = transportation.Id,
        });

        await _transportationsRepository.UpdateAsync(transportation);
        await _transportationsRepository.SaveAsync();
        
        return new TransportationOrderResult()
        {
            Result = ApiResult.Success
        };
    }
}