namespace UseCases.Handlers.Records.Dto
{
    using System.Collections.Generic;

    public class FreeTimePointsResponceDto
    {
        // Строки это даты формата iso (возможно имеет смысл поменять на TimeSpan-ы, но тогда на вебе придется заморочиться)
        public List<string> FreeTimePoints { set; get; }
    }
}
