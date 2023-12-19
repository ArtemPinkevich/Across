namespace UseCases.Handlers.Records.Dto
{
    public class RecordWashServiceDto
    {
        public int Id { set; get; }

        public bool Enabled { set; get; }

        public string Name { set; get; }

        public int DurationMinutes { set; get; }

        public int Price { set; get; }

        public string Description { set; get; }
    }
}
