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

        [Obsolete("")]
        [HttpPost("shipper")]
        public async Task<RegistrationDto> Register([FromBody] ShipperRegistrationCommand shipperRegistrationCommand)
        {
            var res = await _mediator.Send(shipperRegistrationCommand);
            return res;
        }

        [Obsolete("")]
        [HttpPost("driver")]
        public async Task<RegistrationDto> Register([FromBody] DriverRegistrationCommand driverRegistrationCommand)
        {
            var res = await _mediator.Send(driverRegistrationCommand);
            return res;
        }
        
        [Obsolete("")]
        [HttpPost("admin")]
        public async Task<RegistrationDto> Register([FromBody] AdminRegistrationCommand adminRegistrationCommand)
        {
            var res = await _mediator.Send(adminRegistrationCommand);
            return res;
        }


    }
}
