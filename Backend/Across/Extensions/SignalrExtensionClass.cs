using Across.SignalRHubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Across.Extensions
{
    public static class SignalrExtension
    {
        public static void AddSignalRWithAddition(this IServiceCollection services)
        {
            services.AddSingleton<IUserIdProvider, SignalrUserIdProvider>();
            services.AddSignalR();
        }
    }
}
