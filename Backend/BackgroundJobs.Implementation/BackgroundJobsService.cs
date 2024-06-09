using BackgroundJobs.Interfaces;
using Hangfire;

namespace BackgroundJobs.Implementation;

public class BackgroundJobsService : IBackgroundJobsService
{
    public void Enqueue<T>()  where T : IBackgroundJob
    {
        BackgroundJob.Enqueue<T>((x) => x.ExecuteAsync());
    }
}