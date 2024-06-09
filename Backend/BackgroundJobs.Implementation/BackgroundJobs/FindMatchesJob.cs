using BackgroundJobs.Interfaces.BackgroundJobs;
using DataAccess.BaseImplementation;

namespace BackgroundJobs.Implementation.BackgroundJobs;

public class FindMatchesJob : IFindMatchesJob
{
    private readonly DatabaseContext _databaseContext;
    
    public FindMatchesJob(DatabaseContext context)
    {
        _databaseContext = context;
    }
    
    public Task ExecuteAsync()
    {
        Console.WriteLine("Hello from hangfire");
        return Task.CompletedTask;
    }
}