using System.IO;
using System.Threading.Tasks;
using Entities;
using Infrastructure.Interfaces.ReportFileGenerator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.Handlers.EventJournal.Dto;
using UseCases.Handlers.EventJournal.Queries;

namespace BackendWashMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventJournalController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EventJournalController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("download_report_csv/{carWashId}")]
    public async Task<IActionResult> DownloadReportCsv(int carWashId)
    {
        //#TODO this feature will be in release 3.0
        return null;
        
        DownloadReportResultDto result = await _mediator.Send(new DownloadReportQuery(){CarWashId = carWashId, Format = ReportFileFromat.CSV});
        var contentType = "application/csv";
        return File(result.FileStream, contentType, result.Filename);
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("download_report_pdf/{carWashId}")]
    public async Task<IActionResult> DownloadReportPdf(int carWashId)
    {
        //#TODO this feature will be in release 3.0
        return null;

        DownloadReportResultDto result = await _mediator.Send(new DownloadReportQuery(){CarWashId = carWashId, Format = ReportFileFromat.PDF});
        var contentType = "application/pdf";
        return File(result.FileStream, contentType, result.Filename);
    }
    
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("download_report_txt/{carWashId}")]
    public async Task<IActionResult> DownloadReportTxt(int carWashId)
    {
        //#TODO this feature will be in release 3.0
        return null;

        DownloadReportResultDto result = await _mediator.Send(new DownloadReportQuery(){CarWashId = carWashId, Format = ReportFileFromat.TXT});
        var contentType = "application/txt";
        return File(result.FileStream, contentType, result.Filename);
    }
}