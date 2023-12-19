namespace UseCases.Handlers.Vehicles.Queries
{
    using MediatR;
    using System.Collections.Generic;
    using UseCases.Handlers.Vehicles.Dto;

    public class GetCarBodiesQuery : IRequest<List<CarBodyDto>>
    {
    }
}
