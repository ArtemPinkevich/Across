namespace BackendWashMe.SignalRHubs
{
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;
    using UseCases.Handlers.CarWash.Dto;
    using UseCases.Handlers.CarWash.Queries;

    public partial class WashMeHub
    {
        [Authorize(Roles = UserRoles.Admin)]
        public async Task car_washes_list_request()
        {
            await Clients.Caller.SendAsync("car_washes_list_response", await _mediator.Send(new GetCarWashesQuery()));
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<CarWashesListResultDto> car_washes_by_user_request()
        {
            var userId = Context.UserIdentifier;
            CarWashesListResultDto result = await _mediator.Send(new GetCarWashesByUserQuery(userId));
            return result;
        }

        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task set_favorite_carwashes(int[] carWashIds)
        {
            var userId = Context.UserIdentifier;
            await _mediator.Send(new SetFavoriteCarwashesCommand(userId, carWashIds));
        }

        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<CarWashesListResultDto> get_favorite_carwashes()
        {
            var userId = Context.UserIdentifier;
            CarWashesListResultDto result = await _mediator.Send(new GetFavoriteCarWashesQuery(userId));
            return result;
        }

        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<CarWashDto> get_car_wash_info(int carWashId)
        {
            CarWashDto result = await _mediator.Send(new GetCarWashSettingsQuery(carWashId));
            return result;
        }

        // TODO объединить с get_car_wash_info
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<CarWashDto> car_wash_settings_request(int carWashId)
        {
            CarWashDto result = await _mediator.Send(new GetCarWashSettingsQuery(carWashId));
            return result;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task update_car_wash_settings_request(CarWashDto carWashDto)
        {
            CarWashDto result = await _mediator.Send(new UpdateCarWashSettingsCommand(carWashDto));
            var broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByCarWash(carWashDto.Id);
            var mobioleClients = _messageReceiversCreatorService.GetMobilesByFavoriteCarwashId(carWashDto.Id);
            broadcastReceivers.AddRange(mobioleClients);
            await Clients.Users(broadcastReceivers).SendAsync("car_wash_settings_broadcast", result);
        }
    }
}
