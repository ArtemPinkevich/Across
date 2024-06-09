namespace BackgroundJobs.Interfaces;

public interface IBackgroundJobsService
{
    void Enqueue<T>() where T : IBackgroundJob;
}