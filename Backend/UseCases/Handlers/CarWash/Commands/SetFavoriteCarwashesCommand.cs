namespace UseCases.Handlers.CarWash.Queries
{
    using MediatR;
    using System.Collections.Generic;

    public class SetFavoriteCarwashesCommand : IRequest<bool>
    {
        public List<int> CarWashIds { get; }

        public string UserId { get; }

        public SetFavoriteCarwashesCommand(string id, int[] carWashIds)
        {
            UserId = id;
            CarWashIds = new List<int>(carWashIds);
        }
    }
}
