using Microsoft.VisualStudio.TestTools.UnitTesting;
using UseCases.Handlers.Profiles.Dto;
using AutoMapper;
using NUnit.Framework;
using UseCases.Handlers.Profiles.Mappings;
using UseCases.Handlers.Truck.Mappings;
using UseCases.Handlers.TransportationOrder.Mapping;

namespace UseCases.Handlers.Mapping.Tests
{
    [TestClass()]
    public class AutoMapperConfigurationsTests
    {
        [Test]
        public void AutoMapper_UserProfileConfiguration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfileAutoMapperProfile>();
                cfg.AddGlobalIgnore(nameof(ProfileDto.Role));
                cfg.AddGlobalIgnore(nameof(ProfileDto.DocumentDtos));
            });
            configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void AutoMapper_TruckConfiguration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<TruckAutoMapperProfile>();
                    cfg.AddGlobalIgnore(nameof(Entities.Truck.DriverId));
                    cfg.AddGlobalIgnore(nameof(Entities.Truck.Driver));
                });
            configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void AutoMapper_OrderConfiguration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<TransportationOrderMapperProfile>());
            configuration.AssertConfigurationIsValid();
        }
    }
}