using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Verification.Dto;

namespace UseCases.Handlers.Verification.Commands;

public class VerifyPhoneCommandHandler: IRequestHandler<VerifyPhoneCommand, VerificationResultDto>
{
    private readonly UserManager<User> _userManager;
    
    public VerifyPhoneCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<VerificationResultDto> Handle(VerifyPhoneCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user == null)
        {
            return new VerificationResultDto()
            {
                Result = ApiResult.Failed,
                Errors = new[] { $"user not found with phone {request.PhoneNumber}" }
            };
        }
        
        var result = await _userManager.ChangePhoneNumberAsync(user, request.PhoneNumber, request.Code);
        if (result.Succeeded)
            return new VerificationResultDto() { Result = ApiResult.Success };

        return new VerificationResultDto()
        {
            Result = ApiResult.Failed,
            Errors = result.Errors.Select(x => x.Description).ToArray()
        };
    }
}