using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using UseCases.Handlers.Profiles.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Profiles.Commands;

internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ProfileResultDto>
{
    private readonly UserManager<User> _userManager;

    public UpdateProfileCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<ProfileResultDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                user.Name = request.ProfileDto.Name;
                user.Surname = request.ProfileDto.Surname;
                user.Patronymic = request.ProfileDto.Patronymic;
                user.BirthDate = request.ProfileDto.BirthDate;
                user.ReservePhoneNumber = request.ProfileDto.ReservePhoneNumber;

                await _userManager.UpdateAsync(user);
            }
        }
        catch (Exception exc)
        {
            return new ProfileResultDto() { Result = ApiResult.Failed };
        }

        return new ProfileResultDto() { Result = ApiResult.Success };
    }
}
