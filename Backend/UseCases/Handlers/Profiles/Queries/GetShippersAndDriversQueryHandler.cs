using System.Collections.Generic;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Handlers.Profiles.Queries;

public class GetShippersAndDriversQueryHandler : IRequestHandler<GetShippersAndDriversQuery, List<ProfileDto>>
{
    private readonly UserManager<User> _userManager;

    public GetShippersAndDriversQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<ProfileDto>> Handle(GetShippersAndDriversQuery request, CancellationToken cancellationToken)
    {
        var shippersAndDrivers = new List<ProfileDto>();

        foreach (var user in _userManager.Users.Include(x => x.Documents))
        {
            var userRole = await _userManager.GetUserRole(user);
            if (userRole == null)
            {
                continue;
            }

            if (userRole != UserRoles.Driver && userRole != UserRoles.Shipper)
            {
                continue;
            }

            shippersAndDrivers.Add(new ProfileDto()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                Role = userRole,
                Status = user.UserStatus,
                DocumentDtos = userRole == UserRoles.Driver ? UserDocumentsHelper.CreateDriverDocumentsList(user) : UserDocumentsHelper.CreateShipperDocumentsList(user)
            });
        }

        return shippersAndDrivers;
    }
}
