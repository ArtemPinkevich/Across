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
            var user = await _userManager.Users
                .Include(x => x.LegalInformation)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user != null)
            {
                user.Name = request.ProfileDto.Name;
                user.Surname = request.ProfileDto.Surname;
                user.Patronymic = request.ProfileDto.Patronymic;
                user.BirthDate = request.ProfileDto.BirthDate;
                user.ReservePhoneNumber = request.ProfileDto.ReservePhoneNumber;
                user.LegalInformation ??= new LegalInformation();
                if (request.ProfileDto.LegalInformationDto != null)
                {
                    user.LegalInformation.Bin = request.ProfileDto.LegalInformationDto.Bin ?? String.Empty;
                    user.LegalInformation.Email = request.ProfileDto.LegalInformationDto.Email ?? String.Empty;
                    user.LegalInformation.AccountNumber = request.ProfileDto.LegalInformationDto.AccountNumber ?? String.Empty;
                    user.LegalInformation.BankBin = request.ProfileDto.LegalInformationDto.BankBin ?? String.Empty;
                    user.LegalInformation.BankName = request.ProfileDto.LegalInformationDto.BankName ?? String.Empty;
                    user.LegalInformation.CompanyCeo = request.ProfileDto.LegalInformationDto.CompanyCeo ?? String.Empty;
                    user.LegalInformation.CompanyName = request.ProfileDto.LegalInformationDto.CompanyName ?? String.Empty;
                    user.LegalInformation.LegalAddress = request.ProfileDto.LegalInformationDto.LegalAddress ?? String.Empty;
                    user.LegalInformation.PhoneNumber = request.ProfileDto.LegalInformationDto.PhoneNumber ?? String.Empty;
                    user.LegalInformation.VatSeria = request.ProfileDto.LegalInformationDto.VatSeria ?? String.Empty;
                    user.LegalInformation.BankSwiftCode = request.ProfileDto.LegalInformationDto.BankSwiftCode ?? String.Empty;
                }

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
