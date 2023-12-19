using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Entities;
using Infrastructure.Interfaces.ReportFileGenerator;

namespace Infrastructure.ReportFileGenerator;

public class TxtReportFileGenerator:IReportFileGenerator
{
    private const string _txtLineTemplate =
        "Телефон:{0} Авто:{1} Услуги:{2} Цена:{3} Длительность:{4} мин.";
    
    public Stream GenerateReport(List<Record> records)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var record in records)
        {
            builder.AppendLine(ConvertRecordToString(record));
        }
        return new MemoryStream(Encoding.UTF8.GetBytes(builder.ToString()));
    }

    public string GetFileName()
    {
        return "report.txt";
    }

    private string ConvertRecordToString(Record record)
    {
        StringBuilder servicesBuilder = new StringBuilder();
        foreach (var service in record.WashServices)
        {
            servicesBuilder.Append(service.Name + ", ");
        }
        return String.Format(_txtLineTemplate, record.PhoneNumber, $"{record.Vehicle.Mark},{record.Vehicle.Model}", servicesBuilder, record.TotalPrice, record.TotalDurationMin);
    }
}