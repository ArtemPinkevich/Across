namespace UseCases.Handlers.Vehicles.Commands
{
    using MediatR;
    using System.Collections.Generic;
    using UseCases.Handlers.Records.Dto;

    public class BindCarsWithUserCommand : IRequest<bool>
    {
        public List<CarInfoDto> Cars { get; }

        public string UserId { get; }

        public BindCarsWithUserCommand(string id, List<CarInfoDto> cars)
        {
            UserId = id;
            Cars = cars;
        }
    }
}
