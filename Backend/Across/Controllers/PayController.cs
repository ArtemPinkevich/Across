using System;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Truck.Commands;
using UseCases.Handlers.Truck.Dto;
using UseCases.Handlers.Truck.Queries;
using UseCases.Handlers.Profiles.Queries;
using UseCases.Handlers.Payment.Queries;
using UseCases.Handlers.Payment.Dto;

namespace Across.Controllers;


public class PayResultArgs
{
    public double OutSum { set; get; }
    public int InvId { set; get; }
    public string Fee { set; get; }
    public string EMail { set; get; }
    public string SignatureValue { set; get; }
    public string PaymentMethod { set; get; }
    public string IncCurrLabel { set; get; }
}


[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class PayController : ControllerBase
{
    private readonly IMediator _mediator;

    public PayController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    //[Authorize(Roles = UserRoles.Driver)]
    [HttpPost("pay-result")]
    public async Task<string> PayResult([FromQuery] PayResultArgs args)
    {
        //string userId = HttpContext.User.Claims.FirstOrDefault( x => x.Type == JwtClaimsTypes.Id)?.Value;
        //return await _mediator.Send(new AddOrUpdateTruckToUserCommand()
        //{
        //    UserId = userId,
        //    TruckDto = addTruckToUserDto
        //});

        //var qqq = new HttpResponseMessage(HttpStatusCode.OK);
        // return $"OK{args.InvId}";
        return "OK";
    }

    //[Authorize(Roles = UserRoles.Driver)]
    [HttpPost("pay-success")]
    public async Task<string> PaySuccess([FromQuery] PayResultArgs args)
    {
        return "OK";
    }

    //[Authorize(Roles = UserRoles.Driver)]
    [HttpPost("pay-success-redirect")]
    public IActionResult PaySuccessRedirect([FromQuery] PayResultArgs args)
    {
        return Redirect("~/Home");
    }

    //[Authorize(Roles = UserRoles.Driver)]
    [HttpGet("get-pay-result")]
    public async Task<string> GetPayResult()
    { 
        return "OK";

    }

    //[Authorize(Roles = UserRoles.Driver)]
    [HttpGet("get-pay-success")]
    public async Task<string> GetPaySuccess([FromQuery] PayResultArgs args)
    {
        return "OK";
    }

    //[Authorize(Roles = UserRoles.Driver)]
    [HttpGet("get-payment-expire-date")]
    public async Task<PaymentExpireDateDto> GetPaymentExpireDate()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimsTypes.Id)?.Value;
        PaymentExpireDateDto result = await _mediator.Send(new GetPaymentExpireDateQuery()
        {
            UserId = userId,
        });

        return result;
    }
}
