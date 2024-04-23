using MediatR;
using UseCases.Handlers.Verification.Dto;

namespace UseCases.Handlers.Verification.Commands;

public class VerifyPhoneCommand : IRequest<VerificationResultDto>
{
    public string PhoneNumber { set; get; }
    
    public string Code { set; get; }
}