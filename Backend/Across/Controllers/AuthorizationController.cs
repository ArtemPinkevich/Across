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

        [HttpGet("driver/{phone}/{withVerification}")]
        public async Task<AuthorizationDto> DriverAuthorize(string phone, bool withVerification)
        {
            return await _mediator.Send(new DriverAuthorizationQuery(){ Phone = phone, WithVerification = withVerification });
        }

        [HttpGet("shipper/{phone}/{withVerification}")]
        public async Task<AuthorizationDto> ShipperAuthorize(string phone, bool withVerification)
        {
            return await _mediator.Send(new ShipperAuthorizationQuery() { Phone = phone, WithVerification = withVerification });
        }
        
        [HttpGet("refresh_tokens")]
        public async Task<AuthorizationDto> RefreshTokens()
        {
            return await _mediator.Send(new UpdateAccessTokenQuery());
        }
    }
}
