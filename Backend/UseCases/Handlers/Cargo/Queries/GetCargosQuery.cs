using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.Cargo.Queries;

public class GetCargosQuery : IRequest<CargosListDto>
{
    public string UserId { set; get; }
}