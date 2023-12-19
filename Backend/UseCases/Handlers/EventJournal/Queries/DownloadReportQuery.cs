using Infrastructure.Interfaces.ReportFileGenerator;
using MediatR;
using UseCases.Handlers.EventJournal.Dto;

namespace UseCases.Handlers.EventJournal.Queries;

public class DownloadReportQuery : IRequest<DownloadReportResultDto>
{
    public int CarWashId { set; get; }
    
    public ReportFileFromat Format { set; get; }
}