using MediatR;
using UseCases.Handlers.Verification.Dto;

namespace UseCases.Handlers.Verification.Commands;

public class SendSmsCommand : IRequest<SendSmsCodeResultDto>
{
    public string PhoneNumber { set; get; }
}