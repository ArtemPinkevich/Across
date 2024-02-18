using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.Cargo.Commands;

public class DeleteCargoCommand : IRequest<CargoResult>
{
    public int CargoId { set; get; }
}