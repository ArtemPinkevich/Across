﻿using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class DeleteTruckCommandHandler:IRequestHandler<DeleteTruckCommand, TruckResultDto>
{
    private readonly IRepository<Entities.Truck> _repository;

    public DeleteTruckCommandHandler(IRepository<Entities.Truck> repository)
    {
        _repository = repository;
    }
    
    public async Task<TruckResultDto> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
    {
        var truck = await _repository.GetAsync(x => x.Id == request.TruckId);
        if (truck == null)
        {
            return new TruckResultDto()
            {
                Result = ApiResult.Failed,
                Reasons = new[] { $"no truck with id={request.TruckId} found" }
            };
        }

        truck.IsActive = false;
        await _repository.UpdateAsync(truck);
        await _repository.SaveAsync();
        
        return new TruckResultDto()
        {
            Result = ApiResult.Success,
        };
    }
}