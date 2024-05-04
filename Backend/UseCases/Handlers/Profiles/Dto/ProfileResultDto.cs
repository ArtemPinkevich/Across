using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Profiles.Dto;

public class ProfileResultDto
{
    public ApiResult Result { set; get; }

    public string[] Reasons { set; get; }
}
