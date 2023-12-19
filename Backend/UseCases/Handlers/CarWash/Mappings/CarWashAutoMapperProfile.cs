namespace UseCases.Handlers.CarWash.Mappings
{
    using AutoMapper;
    using Entities;
    using System.Collections.Generic;
    using UseCases.Handlers.CarWash.Dto;
    using CarWash = Entities.CarWash;

    public class CarWashAutoMapperProfile: Profile
    {
        public CarWashAutoMapperProfile()
        {
            CreateMap<List<CarWash>, CarWashesListResultDto>()
                .ForMember(dest => dest.carWashDtos, opt => opt.MapFrom(source => source.ConvertAll(ConvertToCarWashDto)));

            CreateMap<WorkSchedule, WorkTimeDto>();

            CreateMap<CarWash, CarWashDto>()
                .ForMember(dest => dest.WorkTime, opt => opt.MapFrom(source => source.WorkSchedule));
        }

        private CarWashDto ConvertToCarWashDto(CarWash carWash)
        {
            return new CarWashDto()
            {
                Id = carWash.Id,
                Name = carWash.Name,
                Location = carWash.Location,
                Longitude= carWash.Longitude,
                Latitude = carWash.Latitude,
                Phone = carWash.Phone,
                BoxesQuantity = carWash.BoxesQuantity,
                ReservedMinutesBetweenRecords = carWash.ReservedMinutesBetweenRecords,
            };
        }
    }
}
