using Entities.Enums;
using System.Collections.Generic;

namespace UseCases.Handlers.Profiles.Dto
{
    public class ProfileDto
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string Surname { set; get; }

        public string Patronymic { set; get; }

        public string BirthDate { set; get; }
        
        public string Role { set; get; }

        public UserStatus Status { set; get; }

        public string PhoneNumber { set; get; }
        
        public string ReservePhoneNumber { set; get; }

        public List<DocumentDto> DocumentDtos { set; get; }
    }
}
