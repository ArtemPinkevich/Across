namespace UseCases.Handlers.Records.Mapping
{
    using AutoMapper;
    using Entities;
    using System.Linq;
    using UseCases.Handlers.Records.Dto;

    public class RecordAutoMapperProfile : Profile
    {
        public RecordAutoMapperProfile()
        {
            CreateMap<Record, RecordDto>()
                .ForMember(dest => dest.CarInfo, opt => opt.MapFrom(source => source.Vehicle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(source => source.TotalPrice))
                .ForMember(dest => dest.DurationMin, opt => opt.MapFrom(source => source.TotalDurationMin))
                .ForMember(dest => dest.MainServiceId, opt => opt.MapFrom(source => source.MainWashServiceId))
                .ForMember(dest => dest.AdditionServicesIds, opt => opt.MapFrom(src => src.WashServices.Select(o => o.Id).ToList()));
        }
    }
}
