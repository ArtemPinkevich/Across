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

        [HttpGet("refresh_tokens")]
        public async Task<AuthorizationDto> RefreshTokens()
        {
            return await _mediator.Send(new UpdateAccessTokenQuery());
        }

        [HttpPost("auth")]
        public async Task<AuthorizationDto> Authorize([FromBody]AuthorizationQuery authorizationQuery)
        {
            return await _mediator.Send(authorizationQuery);
        }
    }
}
