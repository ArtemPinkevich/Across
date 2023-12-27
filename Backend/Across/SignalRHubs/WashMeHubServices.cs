namespace BackendWashMe.SignalRHubs
{
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UseCases.Handlers.WashServices.Commands;
    using UseCases.Handlers.WashServices.Dto;
    using UseCases.Handlers.WashServices.Queries;

    public partial class WashMeHub
    {
        [Authorize(Roles = UserRoles.Admin)]
        public async Task get_services(int carwashId)
        {
            GetWashServicesDto result = await _mediator.Send(new GetWashServicesQuery(carwashId, true));
            await Clients.Caller.SendAsync("wash_services_broadcast", result);
        }

        // TODO объединить с get_services
        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<GetWashServicesDto> get_services_directly(int carwashId)
        {
            GetWashServicesDto result = await _mediator.Send(new GetWashServicesQuery(carwashId, false));
            return result;
        }

        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<List<WashServiceOnePriceDto>> get_services_by_car_body(int carwashId, int carBodyId)
        {
            List<WashServiceOnePriceDto> result = await _mediator.Send(new GetWashServicesByCarBodyQuery(carwashId, carBodyId));
            return result;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task add_wash_service(WashServiceDto washService, int carWashId)
        {
            var broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByCarWash(carWashId);
            AddOrUpdateWashServiceResultDto result = await _mediator.Send(new AddOrUpdateWashServiceCommand(carWashId, washService));
            await Clients.Users(broadcastReceivers).SendAsync("add_wash_service_responce", result.WashService);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task remove_service(int id)
        {
            var broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByWashService(id);
            RemoveWashServiceResultDto result = await _mediator.Send(new RemoveWashServiceCommand(id));
            await Clients.Users(broadcastReceivers).SendAsync("remove_wash_service_responce", result.RemovedWashServiceId);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task update_wash_service(WashServiceDto washService, int carWashId)
        {
            var broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByCarWash(carWashId);
            AddOrUpdateWashServiceResultDto result = await _mediator.Send(new AddOrUpdateWashServiceCommand(carWashId, washService));
            if (result != null)
            {
                await Clients.Users(broadcastReceivers).SendAsync("update_wash_service_responce", result.WashService);
            }
        }
    }
}
