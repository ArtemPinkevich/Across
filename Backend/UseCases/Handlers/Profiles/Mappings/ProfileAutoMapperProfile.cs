using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;

namespace UseCases.Handlers.Profiles.Mappings;

#warning add after map action
public class SetUserRoleAndDocumentsAfterMap : IMappingAction<User, ProfileDto>
{
    private readonly UserManager<User> _userManager;

    public SetUserRoleAndDocumentsAfterMap(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public void Process(User source, ProfileDto destination, ResolutionContext context)
    {
        var role = _userManager.GetUserRole(source).Result;
        destination.Role = role;
        destination.DocumentDtos = role == UserRoles.Driver
            ? UserDocumentsHelper.CreateDriverDocumentsList(source)
            : UserDocumentsHelper.CreateShipperDocumentsList(source);

    }
}

public class ProfileAutoMapperProfile : Profile
{
    public ProfileAutoMapperProfile()
    {
        CreateMap<User, ProfileDto>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Patronymic, opt => opt.MapFrom(s => s.Patronymic))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.UserStatus))
            .ForMember(d => d.Surname, opt => opt.MapFrom(s => s.Surname))
            .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.ReservePhoneNumber, opt => opt.MapFrom(s => s.ReservePhoneNumber))
            .ReverseMap();
    }
}