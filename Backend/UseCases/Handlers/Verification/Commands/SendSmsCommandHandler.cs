using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Verification.Dto;

namespace UseCases.Handlers.Verification.Commands;

public class SendSmsCommandHandler: IRequestHandler<SendSmsCommand, SendSmsCodeResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly ISmsGateway _smsGateway;

    public SendSmsCommandHandler(UserManager<User> userManager,
        ISmsGateway smsGateway)
    {
        _userManager = userManager;
        _smsGateway = smsGateway;
    }
    
    public async Task<SendSmsCodeResultDto> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user == null)
        {
            user = CreateUser(request);
            IdentityResult userResult =  await _userManager.CreateAsync(user);
            if (userResult != IdentityResult.Success)
                return CreateErrorCreateUserResult(userResult);

            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, UserRoles.Driver);
            if (roleResult != IdentityResult.Success)
                return CreateErrorCreateUserResult(roleResult);
        }

        var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
        await _smsGateway.SendSms(request.PhoneNumber, code);
        
        return new SendSmsCodeResultDto()
        {
            Result = ApiResult.Success,
            Errors = null
        };
    }
    
    private User CreateUser(SendSmsCommand request)
    {
        return new User()
        {
            UserName = request.PhoneNumber,
            PhoneNumber = request.PhoneNumber,
            LegalInformation = new LegalInformation()
        };
    }

    private SendSmsCodeResultDto CreateErrorCreateUserResult(IdentityResult result)
    {
        return new SendSmsCodeResultDto()
        {
            Result = ApiResult.Failed,
            Errors = result.Errors.Select(x => x.Description).ToArray()
        };
    }
}