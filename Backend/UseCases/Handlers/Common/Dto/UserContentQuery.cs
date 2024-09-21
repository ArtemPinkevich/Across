using Entities;

namespace UseCases.Handlers.Common.Dto
{
    public class UserContentQuery
    {
        public UserContentType DocumentType { get; set; }
        public string SectionKey { get; set; }
        public string UserId { get; set; }
    }
}
