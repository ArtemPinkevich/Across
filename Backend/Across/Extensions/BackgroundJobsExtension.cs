using BackgroundJobs.Implementation;
using BackgroundJobs.Implementation.BackgroundJobs;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Interfaces.BackgroundJobs;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Across.Extensions;

public static class BackgroundJobsExtension
{
    public static void AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(cfg => cfg.UseMemoryStorage());
        services.AddHangfireServer();

        services.AddScoped<IBackgroundJobsService, BackgroundJobsService>();
        services.AddScoped<IFindMatchesJob, FindMatchesJob>();
    }
}