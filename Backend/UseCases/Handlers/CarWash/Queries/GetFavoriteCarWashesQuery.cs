namespace UseCases.Handlers.CarWash.Queries
{
    using MediatR;
    using UseCases.Handlers.CarWash.Dto;

    public class GetFavoriteCarWashesQuery : IRequest<CarWashesListResultDto>
    {
        public string UserId { get; }

        public GetFavoriteCarWashesQuery(string id)
        {
            UserId = id;
        }
    }
}
