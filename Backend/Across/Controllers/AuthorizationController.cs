﻿using System;
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

        [HttpGet("web/{login}/{password}")]
        public async Task<AuthorizationDto> Authorize(string login, string password)
        {
            return await _mediator.Send(new AuthorizationQuery() { Login = login, Password = password });
        }

        [HttpGet("mobile/{phone}")]
        public async Task<AuthorizationDto> MobileAuthorize(string phone)
        {
            ShipperAuthorizationQuery auth = new ShipperAuthorizationQuery() { Phone = phone };
            return await _mediator.Send(auth);
        }
    }
}
