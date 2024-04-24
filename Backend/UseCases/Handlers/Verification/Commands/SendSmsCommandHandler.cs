using System.Threading;
using System.Threading.Tasks;
using Entities;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Verification.Dto;

namespace UseCases.Handlers.Verification.Commands;

public class SendSmsCommandHandler: IRequestHandler<SendSmsCommand, VerificationResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly ISmsGateway _smsGateway;

    public SendSmsCommandHandler(UserManager<User> userManager,
        ISmsGateway smsGateway)
    {
        _userManager = userManager;
        _smsGateway = smsGateway;
    }
    
    public async Task<VerificationResultDto> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user == null)
            return new VerificationResultDto()
            {
                Result = ApiResult.Failed,
                Errors = new[] { $"No user found with phone {request.PhoneNumber}" }
            };
        
        var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
        await _smsGateway.SendSms(request.PhoneNumber, code);
        
        return new VerificationResultDto()
        {
            Result = ApiResult.Success,
            Errors = null
        };
    }
}