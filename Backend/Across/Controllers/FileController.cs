using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Profiles.Commands;

namespace Across.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Shipper}")]
    [HttpPost("upload-user-content")]
    public async Task<IActionResult> UploadUserContent([FromForm(Name = "image")] IFormFile formFile, [FromQuery] UserContentQuery query)
    {
        if (formFile == null)
        {
            return StatusCode(400);
        }

        var userFolderPath = GetUserFolderPath();
        if (string.IsNullOrEmpty(userFolderPath))
        {
            return StatusCode(500);
        }

        var resultPath = string.IsNullOrEmpty(query.SectionKey) ? userFolderPath : GetSectionFolderPath(userFolderPath, query.SectionKey);
        if (string.IsNullOrEmpty(resultPath))
        {
            return StatusCode(500);
        }

        var extension = Path.GetExtension(formFile.FileName);
        var filePath = Path.Combine(resultPath, $"{query.ContentType}_{DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss")}{extension}");
        if (System.IO.File.Exists(filePath))
        {
            return StatusCode(500);
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }

        if (query.ContentType == UserContentType.DriverLicence
            || query.ContentType == UserContentType.PassportMain
            || query.ContentType == UserContentType.PassportBackSide
            || query.ContentType == UserContentType.AdrCertificate)
        {
            await _mediator.Send(new AddOrUpdateDocument()
            {
                UserId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimsTypes.Id)?.Value,
                Comment = "",
                DocumentStatus = 1,
                DocumentType = query.ContentType
            });
        }

        return StatusCode(200);
    }

    [Authorize(Roles = $"{UserRoles.Driver},{UserRoles.Shipper},{UserRoles.Lawyer},{UserRoles.Admin},{UserRoles.Owner}")]
    [HttpGet("get-user-content")]
    public IActionResult GetImage([FromQuery] UserContentQuery query)
    {
        var userId = query.UserId;
        if (userId == null)
        {
            userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimsTypes.Id)?.Value;
        }

        if (userId == null)
        {
            return StatusCode(500);
        }

        var userFolderPath = string.IsNullOrEmpty(query.UserId) ? GetUserFolderPath() : Path.Combine(Directory.GetCurrentDirectory() + "Files", query.UserId);

        if (string.IsNullOrEmpty(userFolderPath) || !Directory.Exists(userFolderPath))
        {
            return NotFound();
        }

        var resultPath = string.IsNullOrEmpty(query.SectionKey) ? userFolderPath : GetSectionFolderPath(userFolderPath, query.SectionKey);
        if (string.IsNullOrEmpty(resultPath))
        {
            return StatusCode(500);
        }

        var files = Directory.GetFiles(resultPath, $"{query.ContentType}*.*").ToList();
        var fullFileName = files.LastOrDefault();
        if (string.IsNullOrEmpty(fullFileName))
        {
            return NotFound();
        }

        var extensionWithoutDot = Path.GetExtension(fullFileName).Replace(".", string.Empty);
        return PhysicalFile(@$"{fullFileName}", $"image/{extensionWithoutDot}");
    }

    private string GetUserFolderPath()
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory() + "Files");
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimsTypes.Id)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return null;
        }

        var userFolder = Path.Combine(basePath, userId);
        if (!Directory.Exists(userFolder))
        {
            Directory.CreateDirectory(userFolder);
        }

        return userFolder;
    }

    private string GetSectionFolderPath(string userFolderPath, string sectionKey)
    {
        var sectionFolderPath = Path.Combine(userFolderPath, sectionKey?.ToString());
        if (!Directory.Exists(sectionFolderPath))
        {
            Directory.CreateDirectory(sectionFolderPath);
        }

        return sectionFolderPath;
    }
}
