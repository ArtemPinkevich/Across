namespace UseCases.Handlers.PriceGroups.Dto
{
    using System.Collections.Generic;

    public class GetPriceGroupsResultDto
    {
        public List<PriceGroupDto> AllPriceGroups { set; get; }
    }
}
