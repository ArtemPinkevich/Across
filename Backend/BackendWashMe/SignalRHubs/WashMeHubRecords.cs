namespace BackendWashMe.SignalRHubs
{
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Commands;
    using UseCases.Handlers.Records.Dto;
    using UseCases.Handlers.Records.Queries;

    public partial class WashMeHub
    {
        [Authorize(Roles = UserRoles.Admin)]
        public async Task get_records(int carWashId, string selectedDate)
        {
            List<RecordDto> result = await _mediator.Send(new GetRecordsQuery(carWashId, selectedDate));
            await Clients.Caller.SendAsync("records_responce", result);
        }

        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<List<RecordDto>> get_records_by_phone(string phoneNumber)
        {
            List<RecordDto> result = await _mediator.Send(new GetRecordsByPhoneQuery(phoneNumber));
            return result;
        }

        [Authorize(Roles = UserRoles.All)]
        public async Task<FreeTimePointsResponceDto> free_time_points_request(int carWashId, string selectedDate, int durationMin)
        {
            FreeTimePointsResponceDto result = await _mediator.Send(new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin));
            return result;
        }

        [Authorize(Roles = UserRoles.All)]
        public async Task update_record_request(RecordDto recordDto)
        {
            List<string> broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByCarWash(recordDto.CarWashId);
            broadcastReceivers.AddRange(_messageReceiversCreatorService.GetMobilesByPhone(recordDto.PhoneNumber));
            RecordDto result = await _mediator.Send(new AddOrUpdateRecordCommand(recordDto.CarWashId, recordDto));
            await Clients.Users(broadcastReceivers).SendAsync("record_broadcast", result);
        }

        [Authorize(Roles = UserRoles.All)]
        public async Task discard_record_request(int recordId)
        {
            List<string> broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByRecordId(recordId);
            List<string> mobileUserIds = await _messageReceiversCreatorService.GetMobilesByRecordId(recordId);
            broadcastReceivers.AddRange(mobileUserIds);
            DiscardRecordResultDto result = await _mediator.Send(new DiscardRecordCommand(recordId));
            await Clients.Users(broadcastReceivers).SendAsync("discard_record_broadcast", result);
        }
    }
}
