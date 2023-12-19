using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Infrastructure.Interfaces.ReportFileGenerator;
using MediatR;
using UseCases.Handlers.EventJournal.Dto;

namespace UseCases.Handlers.EventJournal.Queries;

public class DownloadReportQueryHandler : IRequestHandler<DownloadReportQuery, DownloadReportResultDto>
{
    private readonly IRepository<Record> _recordsRepository;
    private readonly IReportGeneratorFactory _reportGeneratorFactory;

    public DownloadReportQueryHandler(IRepository<Record> recordsRepository,
        IReportGeneratorFactory reportGeneratorFactory)
    {
        _recordsRepository = recordsRepository;
        _reportGeneratorFactory = reportGeneratorFactory;
    }
    
    public async Task<DownloadReportResultDto> Handle(DownloadReportQuery request, CancellationToken cancellationToken)
    {
        var reportFileGenerator = _reportGeneratorFactory.GetReportFileGenerator(request.Format);
        var records = await  _recordsRepository.GetAllAsync(x => x.CarWashId == request.CarWashId);

        return new DownloadReportResultDto()
        {
            FileStream = reportFileGenerator.GenerateReport(records),
            Filename = reportFileGenerator.GetFileName()
        };
    }
}