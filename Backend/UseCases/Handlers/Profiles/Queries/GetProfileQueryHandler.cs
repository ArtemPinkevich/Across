using System.Collections.Generic;
using System.Linq;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using Entities.Document;
using Microsoft.EntityFrameworkCore;
using UseCases.Exceptions;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Queries;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileDto>
{
    private readonly UserManager<User> _userManager;

    public GetProfileQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        User user = await _userManager.Users
            .Include(x => x.Documents)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            return null;
        }

        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = $"Error user role identification {user.UserName}" };
        
        return new ProfileDto()
        {
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            BirthDate = user.BirthDate,
            PhoneNumber = user.PhoneNumber,
            Role = userRole,
            DocumentDtos = userRole == UserRoles.Driver ? CreateDriverDocumentsList(user) : CreateShipperDocumentsList(user)
        };
    }

    private static List<DocumentDto> CreateDriverDocumentsList(User user)
    {
        List<DocumentDto> documents = new List<DocumentDto>();
        var driverLicense = user.Documents.FirstOrDefault(x => x.DocumentType == DocumentType.DriverLicence) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None, DocumentType = DocumentType.DriverLicence, Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = driverLicense.DocumentStatus,
            DocumentType = driverLicense.DocumentType,
            Comment = driverLicense.Comment
        });
        
        var passportMain = user.Documents.FirstOrDefault(x => x.DocumentType == DocumentType.PassportMain) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None, DocumentType = DocumentType.PassportMain, Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = passportMain.DocumentStatus,
            DocumentType = passportMain.DocumentType,
            Comment = passportMain.Comment
        });
        
        var passportRegistration = user.Documents.FirstOrDefault(x => x.DocumentType == DocumentType.PassportRegistration) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None, DocumentType = DocumentType.PassportRegistration, Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = passportRegistration.DocumentStatus,
            DocumentType = passportRegistration.DocumentType,
            Comment = passportRegistration.Comment
        });

        var taxPayerIdentificationNumber = user.Documents.FirstOrDefault(x => x.DocumentType == DocumentType.TaxPayerIdentificationNumber) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None, DocumentType = DocumentType.TaxPayerIdentificationNumber, Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = taxPayerIdentificationNumber.DocumentStatus,
            DocumentType = taxPayerIdentificationNumber.DocumentType,
            Comment = taxPayerIdentificationNumber.Comment
        });
        
        return documents;
    }

    private List<DocumentDto> CreateShipperDocumentsList(User user)
    {
        List<DocumentDto> documents = new List<DocumentDto>();
        return documents;
    }
}
