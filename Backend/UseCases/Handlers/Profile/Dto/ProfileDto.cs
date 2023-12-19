namespace UseCases.Handlers.Profile.Dto
{
    using Entities.Enums;

    public class ProfileDto
    {
        public string Name { set; get; }

        public string Surname { set; get; }

        public string Patronymic { set; get; }

        public string BirthDate { set; get; }

        public Gender Gender { set; get; }
    }
}
