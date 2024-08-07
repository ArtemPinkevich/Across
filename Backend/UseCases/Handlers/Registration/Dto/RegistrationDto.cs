using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Registration.Dto
{
    public class RegistrationDto
    {
        public ApiResult Result { set; get; }

        public string[] Reasons { set; get; }
    }
}
