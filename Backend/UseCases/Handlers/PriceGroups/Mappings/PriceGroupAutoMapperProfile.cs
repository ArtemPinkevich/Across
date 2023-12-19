namespace UseCases.Handlers.PriceGroups.Mappings
{
    using AutoMapper;
    using Entities;
    using System.Collections.Generic;
    using UseCases.Handlers.PriceGroups.Dto;

    public class PriceGroupAutoMapperProfile : Profile
    {
        public PriceGroupAutoMapperProfile()
        {
            CreateMap<PriceGroup, PriceGroupDto>();
            CreateMap<PriceGroupDto, PriceGroup>();

            CreateMap<List<PriceGroup>, GetPriceGroupsResultDto>()
                .ForMember(dest => dest.AllPriceGroups, opt => opt.MapFrom(src => src));

        }
    }
}
