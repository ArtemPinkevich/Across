namespace BackendWashMe.SignalRHubs
{
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Dto;
    using UseCases.Handlers.Vehicles.Commands;
    using UseCases.Handlers.Vehicles.Dto;
    using UseCases.Handlers.Vehicles.Queries;

    public partial class WashMeHub
    {
        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task set_cars(List<CarInfoDto> cars)
        {
            var userId = Context.UserIdentifier;
            await _mediator.Send(new BindCarsWithUserCommand(userId, cars));
        }

        public async Task<List<CarBodyDto>> get_car_bodies()
        {
            List<CarBodyDto> result = await _mediator.Send(new GetCarBodiesQuery());
            return result;
        }
    }
}
