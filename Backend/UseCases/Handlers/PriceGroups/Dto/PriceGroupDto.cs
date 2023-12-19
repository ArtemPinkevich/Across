namespace UseCases.Handlers.PriceGroups.Dto
{
    using System.Collections.Generic;
    using UseCases.Handlers.Vehicles.Dto;

    public class PriceGroupDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public List<CarBodyDto> CarBodies { set; get; }
    }
}
