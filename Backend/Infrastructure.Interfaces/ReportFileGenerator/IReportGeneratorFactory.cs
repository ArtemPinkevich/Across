namespace Infrastructure.Interfaces.ReportFileGenerator;

public interface IReportGeneratorFactory
{
    IReportFileGenerator GetReportFileGenerator(ReportFileFromat fileFromat);
}