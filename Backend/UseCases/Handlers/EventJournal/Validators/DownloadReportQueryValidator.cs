using FluentValidation;
using UseCases.Handlers.EventJournal.Queries;

namespace UseCases.Handlers.EventJournal.Validators;

public class DownloadReportQueryValidator: AbstractValidator<DownloadReportQuery>
{
    public DownloadReportQueryValidator()
    {
        RuleFor(x => x.CarWashId).GreaterThan(0);
    }
}