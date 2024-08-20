using Entities.Document;
using Entities;
using System.Collections.Generic;
using UseCases.Handlers.Profiles.Dto;
using System.Linq;

namespace UseCases.Handlers.Profiles.Helpers;

public class UserDocumentsHelper
{
    public static List<DocumentDto> CreateDriverDocumentsList(User user)
    {
        List<DocumentDto> documents = new List<DocumentDto>();
        var driverLicense = user.Documents?.FirstOrDefault(x => x.DocumentType == DocumentType.DriverLicence) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None,
            DocumentType = DocumentType.DriverLicence,
            Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = driverLicense.DocumentStatus,
            DocumentType = driverLicense.DocumentType,
            Comment = driverLicense.Comment
        });

        var passportMain = user.Documents?.FirstOrDefault(x => x.DocumentType == DocumentType.PassportMain) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None,
            DocumentType = DocumentType.PassportMain,
            Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = passportMain.DocumentStatus,
            DocumentType = passportMain.DocumentType,
            Comment = passportMain.Comment
        });

        var passportRegistration = user.Documents?.FirstOrDefault(x => x.DocumentType == DocumentType.PassportRegistration) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None,
            DocumentType = DocumentType.PassportRegistration,
            Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = passportRegistration.DocumentStatus,
            DocumentType = passportRegistration.DocumentType,
            Comment = passportRegistration.Comment
        });

        var taxPayerIdentificationNumber = user.Documents?.FirstOrDefault(x => x.DocumentType == DocumentType.TaxPayerIdentificationNumber) ?? new Document()
        {
            DocumentStatus = DocumentStatus.None,
            DocumentType = DocumentType.TaxPayerIdentificationNumber,
            Comment = ""
        };
        documents.Add(new DocumentDto()
        {
            DocumentStatus = taxPayerIdentificationNumber.DocumentStatus,
            DocumentType = taxPayerIdentificationNumber.DocumentType,
            Comment = taxPayerIdentificationNumber.Comment
        });

        return documents;
    }

    public static List<DocumentDto> CreateShipperDocumentsList(User user)
    {
        List<DocumentDto> documents = new List<DocumentDto>();
        return documents;
    }
}
