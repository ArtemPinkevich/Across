using System.Collections.Generic;
using System.IO;
using Entities;
using Infrastructure.Interfaces.ReportFileGenerator;

namespace Infrastructure.ReportFileGenerator;

public class HtmlReportFileGenerator:IReportFileGenerator
{
    public Stream GenerateReport(List<Record> records)
    {
        throw new System.NotImplementedException();
    }

    public string GetFileName()
    {
        throw new System.NotImplementedException();
    }
}