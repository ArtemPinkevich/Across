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

        [HttpGet("driver/{phone}/{password}")]
        public async Task<AuthorizationDto> Authorize(string phone, string password)
        {
            return await _mediator.Send(new DriverAuthorizationQuery(){ Phone = phone, Password = password });
        }

        [HttpGet("shipper/{phone}/{password}")]
        public async Task<AuthorizationDto> MobileAuthorize(string phone, string password)
        {
            return await _mediator.Send(new ShipperAuthorizationQuery() { Phone = phone, Password = password });
        }
        
        [HttpGet("refresh_access_token")]
        public async Task<AuthorizationDto> MobileAuthorize()
        {
            return await _mediator.Send(new UpdateAccessTokenQuery());
        }
    }
}
