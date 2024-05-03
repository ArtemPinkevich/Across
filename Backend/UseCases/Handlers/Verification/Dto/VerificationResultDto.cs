using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Verification.Dto;

public class VerificationResultDto
{
    public string AccessToken { set; get; }
    
    public ApiResult Result { set; get; }
    
    public string[] Errors { set; get; }
}