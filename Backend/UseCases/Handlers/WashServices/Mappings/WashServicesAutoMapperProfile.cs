namespace UseCases.Handlers.WashServices.Mappings
{
    using AutoMapper;
    using Entities;
    using System.Collections.Generic;
    using System.Linq;
    using UseCases.Handlers.WashServices.Dto;

    public class WashServicesAutoMapperProfile: Profile
    {
        public WashServicesAutoMapperProfile()
        {
            CreateMap<List<WashService>, GetWashServicesDto>()
                .ForMember(dest => dest.AllWashServices, opt => opt.MapFrom(src => src.ConvertAll(ConvertToDto)));

            CreateMap<WashServiceSettings, WashServiceSettingsDto>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(x => x.DurationMinutes))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(dest => dest.PriceGroupId, opt => opt.MapFrom(x => x.PriceGroupId))
                .ReverseMap()
                .ForMember(src => src.DurationMinutes, opt => opt.MapFrom(x => x.Duration))
                .ForMember(src => src.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(src => src.PriceGroupId, opt => opt.MapFrom(x => x.PriceGroupId))
                .ForMember(src => src.PriceGroup, opt => opt.Ignore())
                .ForMember(src => src.WashService, opt => opt.Ignore());

            CreateMap<WashService, WashServiceDto>()
                .ForMember(dest => dest.WashServiceSettingsDtos, opt => opt.MapFrom(src => src.WashServiceSettingsList))
                .ReverseMap()
                .ForMember(src => src.WashServiceSettingsList, opt => opt.MapFrom(x => x.WashServiceSettingsDtos))
                .ForMember(src => src.ComplexWashServices, opt => opt.Ignore());
                
            CreateMap<ComplexWashService, WashServiceDto>()
                .ForMember(dest => dest.WashServiceSettingsDtos, opt => opt.MapFrom(src => src.WashServiceSettingsList))
                .ForMember(dest => dest.Composition, opt => opt.MapFrom(src => src.WashServices.Select(o => o.Id).ToList()))
                .ReverseMap()
                .ForMember(src => src.WashServiceSettingsList, opt => opt.MapFrom(x => x.WashServiceSettingsDtos))
                .ForMember(src => src.ComplexWashServices, opt => opt.Ignore());
        }

        private WashServiceDto ConvertToDto(WashService washService)
        {
            var washServicesDto = new WashServiceDto()
            {
                Id = washService.Id,
                Enabled = washService.Enabled,
                Name = washService.Name,
                Description = washService.Description,
            };

            washServicesDto.WashServiceSettingsDtos = WashServiceSettingsDtoList(washService.WashServiceSettingsList);

            if (washService is ComplexWashService complexWashService)
            {
                washServicesDto.Composition = complexWashService.WashServices?.Select(o => o.Id).ToList();
            }

            return washServicesDto;
        }

        private List<WashServiceSettingsDto> WashServiceSettingsDtoList(List<WashServiceSettings> settings)
        {
            var dto = new List<WashServiceSettingsDto>();

            foreach(var setting in settings)
            {
                dto.Add(new WashServiceSettingsDto()
                {
                    Enabled = setting.Enabled,
                    Duration = setting.DurationMinutes,
                    Price = setting.Price,
                    PriceGroupId = setting.PriceGroupId
                });
            }

            return dto;
        }
    }
}
