using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Registration.Commands;
using UseCases.Handlers.Registration.Dto;

namespace Across.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("shipper")]
        public async Task<RegistrationDto> Register([FromBody] ShipperRegistrationCommand shipperRegistrationCommand)
        {
            var res = await _mediator.Send(shipperRegistrationCommand);
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
