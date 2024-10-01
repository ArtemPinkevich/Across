using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;

namespace UseCases.Handlers.Common.Extensions;

public static class UserExtensions
{
    #warning this need to move to mapper profile
    public static async Task<ProfileDto> ConvertToProfileDtoAsync(this User user,
        UserManager<User> userManager,
        IMapper mapper)
    {
        if (user == null)
            return null;
        
        ProfileDto profileDto = mapper.Map<ProfileDto>(user);
        profileDto.Role = await userManager.GetUserRole(user);
        profileDto.DocumentDtos = profileDto.Role == UserRoles.Driver
            ? UserDocumentsHelper.CreateDriverDocumentsList(user)
            : UserDocumentsHelper.CreateShipperDocumentsList(user);
        return profileDto;
    }
    
}