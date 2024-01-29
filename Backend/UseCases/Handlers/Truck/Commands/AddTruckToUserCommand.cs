using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class AddTruckToUserCommand: IRequest<TruckResultDto>
{
    public string UserId { set; get; }
}