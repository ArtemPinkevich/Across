using System.Collections.Generic;
using System.IO;
using Entities;

namespace Infrastructure.Interfaces.ReportFileGenerator;

public interface IReportFileGenerator
{
    Stream GenerateReport(List<Record> records);
    
    string GetFileName();
}