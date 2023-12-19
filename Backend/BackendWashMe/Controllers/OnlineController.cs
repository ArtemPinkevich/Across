namespace BackendWashMe.Controllers
{
    using ApplicationServices;
    using BackendWashMe.SignalRHubs;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UseCases.Handlers.CarWash.Dto;
    using UseCases.Handlers.CarWash.Queries;
    using UseCases.Handlers.Records.Commands;
    using UseCases.Handlers.Records.Dto;
    using UseCases.Handlers.Records.Queries;
    using UseCases.Handlers.WashServices.Dto;
    using UseCases.Handlers.WashServices.Queries;

    [Route("api/[controller]")]
    [ApiController]
    public class OnlineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<WashMeHub> _hubContext;
        private readonly IMessageReceiversCreatorService _messageReceiversCreatorService;

        public OnlineController(IMediator mediator, 
            IHubContext<WashMeHub> hubContext, 
            IMessageReceiversCreatorService messageReceiversCreatorService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _messageReceiversCreatorService = messageReceiversCreatorService ?? throw new ArgumentNullException(nameof(messageReceiversCreatorService));
        }

        [HttpGet("get_carwash_info/{carwashId}")]
        public async Task<CarWashDto> get_carwash_info(int carWashId)
        {
            CarWashDto result = await _mediator.Send(new GetCarWashSettingsQuery(carWashId));
            return result;
        }

        [HttpGet("get_services/{carwashId}")]
        public async Task<GetWashServicesDto> get_services(int carWashId)
        {
            var query = new GetWashServicesQuery(carWashId, false);
            GetWashServicesDto result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("free_time_points_request/{carwashId}/{selectedDate}/{durationMin}")]
        public async Task<FreeTimePointsResponceDto> free_time_points_request(int carWashId, string selectedDate, int durationMin)
        {
            FreeTimePointsResponceDto result = await _mediator.Send(new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin));
            return result;
        }

        [HttpPost("update_record_request")]
        public async Task<RecordDto> update_record_request([FromBody] RecordDto recordDto)
        {
            List<string> broadcastReceivers = await _messageReceiversCreatorService.GetAdminsByCarWash(recordDto.CarWashId);
            broadcastReceivers.AddRange(_messageReceiversCreatorService.GetMobilesByPhone(recordDto.PhoneNumber));
            RecordDto result = await _mediator.Send(new AddOrUpdateRecordCommand(recordDto.CarWashId, recordDto));
            await _hubContext.Clients.Users(broadcastReceivers).SendAsync("record_broadcast", result);
            return result;
        }
    }
}
