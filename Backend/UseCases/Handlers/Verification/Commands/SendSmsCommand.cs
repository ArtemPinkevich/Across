using MediatR;
using UseCases.Handlers.Verification.Dto;

namespace UseCases.Handlers.Verification.Commands;

public class SendSmsCommand : IRequest<VerificationResultDto>
{
    public string PhoneNumber { set; get; }
}