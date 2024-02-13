using System.Collections.Generic;
using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Queries;

public class GetTrucksQuery: IRequest<TrucksListResultDto>
{
    public string UserId { set; get; }
}