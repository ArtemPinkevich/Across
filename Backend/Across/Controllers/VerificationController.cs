using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Verification.Commands;
using UseCases.Handlers.Verification.Dto;

namespace Across.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VerificationController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public VerificationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet("verify/{phone}/{code}")]
    public async Task<VerificationResultDto> VerifyPhone(string phone, string code)
    {
        return await _mediator.Send(new VerifyPhoneCommand(){PhoneNumber = phone, Code = code});
    }

    [HttpPost("send_sms/{phone}")]
    public async Task<SendSmsCodeResultDto> SendSmsCode(string phone)
    {
        return await _mediator.Send(new SendSmsCommand() { PhoneNumber = phone });
    }
}