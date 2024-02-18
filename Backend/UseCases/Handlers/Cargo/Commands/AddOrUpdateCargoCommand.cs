using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.Cargo.Commands;

public class AddOrUpdateCargoCommand:IRequest<CargoResult>
{
    public string UserId { set; get; }
    
    public CargoDto CargoDto { set; get; }
}