namespace BackendWashMe.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using UseCases.Handlers.Registration.Commands;
    using UseCases.Handlers.Registration.Dto;

    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("mobile")]
        public async Task<RegistrationDto> Register([FromBody] MobileRegistrationCommand mobileRegistrationCommand)
        {
            var res = await _mediator.Send(mobileRegistrationCommand);
            return res;
        }

        [HttpPost("admin")]
        public async Task<RegistrationDto> Register([FromBody] AdminRegistrationCommand adminRegistrationCommand)
        {
            var res = await _mediator.Send(adminRegistrationCommand);
            return res;
        }

        [HttpPost("carwash")]
        public async Task<CarWashRegistrationResult> Register([FromBody] CarWashRegistrationCommand carWashRegistrationCommand)
        {
            var res = await _mediator.Send(carWashRegistrationCommand);
            return res;
        }
    }
}
