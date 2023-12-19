namespace UseCases.Handlers.WashServices.Dto
{
    using System.Collections.Generic;

    public class WashServiceOnePriceDto
    {
        public int Id { set; get; }
        public bool Enabled { set; get; }
        public string Name { set; get; }
        public int Price { set; get; }
        public int Duration { set; get; }
        public string Description { set; get; }
        public List<int> Composition { set; get; }
    }
}
