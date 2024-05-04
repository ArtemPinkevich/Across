using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using UseCases.Exceptions;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Common;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Verification.Dto;

namespace UseCases.Handlers.Verification.Commands;

public class VerifyPhoneCommandHandler: IRequestHandler<VerifyPhoneCommand, VerificationResultDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public VerifyPhoneCommandHandler(IOptions<JwtConfiguration> options,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtGenerator = new JwtGenerator(options.Value);
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
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
        if (!result.Succeeded)
        {
            return new VerificationResultDto()
            {
                AccessToken = null,
                Result = ApiResult.Failed,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }
        
        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = $"Error user role identification {user.UserName}" };
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.RefreshTokenKey,
            _jwtGenerator.CreateRefreshToken(user));

        return new VerificationResultDto()
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
            Result = ApiResult.Success,
            Errors = null
        };
    }
}