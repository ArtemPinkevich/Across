using System.IO;

namespace UseCases.Handlers.EventJournal.Dto;

public class DownloadReportResultDto
{
    public Stream FileStream { set; get; }
    
    public string Filename { set; get; }
}