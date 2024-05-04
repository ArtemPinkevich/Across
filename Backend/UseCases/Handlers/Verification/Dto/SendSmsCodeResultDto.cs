using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Verification.Dto;

public class SendSmsCodeResultDto
{
    public ApiResult Result { set; get; }
    
    public string[] Errors { set; get; }
}