using System;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Search.Queries;

namespace Across.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class SearchController: ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Roles = UserRoles.Driver)]
    [HttpGet("search")]
    public async Task<SearchResultDto> Search([FromQuery] SearchQuery searchDto)
    {
        var result = await _mediator.Send(searchDto);

        return result;
    }
}
