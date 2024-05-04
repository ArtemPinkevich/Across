using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Profiles.Dto;

public class ChangeRoleResultDto
{
    public string AccessToken { set; get; }
    
    public ApiResult Result { set; get; }
    
    public string[] Errors { set; get; }
}