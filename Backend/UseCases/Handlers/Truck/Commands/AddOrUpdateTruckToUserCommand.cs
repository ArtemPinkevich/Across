﻿using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class AddOrUpdateTruckToUserCommand: IRequest<TruckResultDto>
{
    public string UserId { set; get; }
    
    public TruckDto TruckDto { set; get; }
}