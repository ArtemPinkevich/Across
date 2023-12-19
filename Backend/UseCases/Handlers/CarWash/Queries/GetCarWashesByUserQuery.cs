namespace UseCases.Handlers.CarWash.Queries
{
    using MediatR;
    using UseCases.Handlers.CarWash.Dto;

    public class GetCarWashesByUserQuery: IRequest<CarWashesListResultDto>
    {
        public GetCarWashesByUserQuery(string id)
        {
            UserId = id;
        }

        public string UserId { get; }
    }
}
