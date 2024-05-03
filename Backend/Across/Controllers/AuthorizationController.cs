using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Authorization.Queries;

namespace Across.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [Obsolete("use for custom driver authorization")]
        [HttpGet("auth/{phone}")]
        public async Task<AuthorizationDto> Authorize(string phone)
        {
            return await _mediator.Send(new AuthorizationQuery(){ Phone = phone });
        }

        [Obsolete("use for custom driver authorization")]
        [HttpGet("driver/{phone}")]
        public async Task<AuthorizationDto> DriverAuthorize(string phone)
        {
            return await _mediator.Send(new DriverAuthorizationQuery(){ Phone = phone });
        }

        [Obsolete("use for custom shipper authorization")]
        [HttpGet("shipper/{phone}")]
        public async Task<AuthorizationDto> ShipperAuthorize(string phone)
        {
            return await _mediator.Send(new ShipperAuthorizationQuery() { Phone = phone });
        }
        
        [HttpGet("refresh_tokens")]
        public async Task<AuthorizationDto> RefreshTokens()
        {
            return await _mediator.Send(new UpdateAccessTokenQuery());
        }
    }
}
