namespace BackgroundJobs.Interfaces;

public interface IBackgroundJob
{
    Task ExecuteAsync();
}