using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Profiles.Commands;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Queries;

namespace Across.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class ProfilesController: ControllerBase
{
    private readonly IMediator _mediator;

    public ProfilesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Shipper}")]
    [HttpGet("get_profile")]
    public async Task<ProfileDto> GetProfile()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new GetProfileQuery()
        {
            UserId = userId,
        });
    }


    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Shipper}")]
    [HttpPost("update_profile")]
    public async Task<ProfileResultDto> AddTruck([FromBody] ProfileDto profileDto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new UpdateProfileCommand()
        {
            UserId = userId,
            ProfileDto = profileDto
        });
    }
    
    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Shipper}")]
    [HttpPost("change_role")]
    public async Task<ChangeRoleResultDto> ChangeRole([FromBody] string role)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        return await _mediator.Send(new ChangeRoleCommand()
        {
            UserId = userId,
            Role = role
        });
    }

    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Lawyer}")]
    [HttpPost("change_doc_status/{docType}/{docStatus}")]
    public async Task<ProfileResultDto> ChangeDocumentStatus([FromBody] ChangeDocumentStatusCommand changeDocumentStatusCommand)
    {
        return await _mediator.Send(changeDocumentStatusCommand);
    }
}
