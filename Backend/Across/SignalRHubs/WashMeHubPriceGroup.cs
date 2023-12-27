namespace BackendWashMe.SignalRHubs
{
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;
    using UseCases.Handlers.PriceGroups.Commands;
    using UseCases.Handlers.PriceGroups.Dto;
    using UseCases.Handlers.PriceGroups.Queries;
    using UseCases.Handlers.WashServices.Commands;

    public partial class WashMeHub
    {
        [Authorize(Roles = UserRoles.Admin)]
        public async Task get_price_groups(int carwashId)
        {
            GetPriceGroupsResultDto result = await _mediator.Send(new GetPriceGroupsQuery(carwashId));
            await Clients.Caller.SendAsync("price_groups_broadcast", result);
        }

        //// TODO объединить с get_services
        //[Authorize(Roles = UserRoles.MobileClient)]
        //public async Task<GetWashServicesDto> get_services_directly(int carwashId)
        //{
        //    GetWashServicesDto result = await _mediator.Send(new GetWashServicesQuery(carwashId, false));
        //    return result;
        //}

        [Authorize(Roles = UserRoles.Admin)]
        public async Task add_price_group(PriceGroupDto priceGroup, int carWashId)
        {
            var broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByCarWash(carWashId);
            AddOrUpdatePriceGroupResultDto result = await _mediator.Send(new AddOrUpdatePriceGroupCommand(carWashId, priceGroup));
            await Clients.Users(broadcastReceivers).SendAsync("add_price_group_responce", result.PriceGroup);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task remove_price_group(int priceGroupId, int carWashId)
        {
            var broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByPriceGroup(priceGroupId);
            RemovePriceGroupResultDto result = await _mediator.Send(new RemovePriceGroupCommand(priceGroupId));
            //await Clients.Users(broadcastReceivers).SendAsync("remove_price_group_responce", result.RemovedPriceGroupId);

            GetPriceGroupsResultDto allGroupsByCarWash = await _mediator.Send(new GetPriceGroupsQuery(carWashId));
            await Clients.Caller.SendAsync("price_groups_broadcast", allGroupsByCarWash);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task update_price_group(PriceGroupDto priceGroupDto, int carWashId)
        {
            var broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByCarWash(carWashId);
            AddOrUpdatePriceGroupResultDto result = await _mediator.Send(new AddOrUpdatePriceGroupCommand(carWashId, priceGroupDto));
            await Clients.Users(broadcastReceivers).SendAsync("update_price_group_responce", result.PriceGroup);
        }
    }
}
