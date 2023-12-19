using Entities;
using Infrastructure.Interfaces.ReportFileGenerator;

namespace Infrastructure.ReportFileGenerator;

public class ReportFileGeneratorFactory : IReportGeneratorFactory
{
    private readonly CsvReportFileGenerator _csvReportFileGenerator;
    private readonly HtmlReportFileGenerator _htmlReportFileGenerator;
    private readonly PdfReportFileGenerator _pdfReportFileGenerator;
    private readonly TxtReportFileGenerator _txtReportFileGenerator;
    
    public ReportFileGeneratorFactory(CsvReportFileGenerator csvReportFileGenerator,
        HtmlReportFileGenerator htmlReportFileGenerator,
        PdfReportFileGenerator pdfReportFileGenerator,
        TxtReportFileGenerator txtReportFileGenerator)
    {
        _csvReportFileGenerator = csvReportFileGenerator;
        _htmlReportFileGenerator = htmlReportFileGenerator;
        _pdfReportFileGenerator = pdfReportFileGenerator;
        _txtReportFileGenerator = txtReportFileGenerator;
    }
        
    public IReportFileGenerator GetReportFileGenerator(ReportFileFromat fileFromat)
    {

        switch (fileFromat)
        {
            case ReportFileFromat.CSV:
                return _csvReportFileGenerator;
            case ReportFileFromat.PDF:
                return _pdfReportFileGenerator;
            case ReportFileFromat.HTML:
                return _htmlReportFileGenerator;
            case ReportFileFromat.TXT:
                return _txtReportFileGenerator;
            default:
                return _txtReportFileGenerator;
        }
    }
}