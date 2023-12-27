namespace BackendWashMe.Extensions
{
    using BackendWashMe.SignalRHubs;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.DependencyInjection;

    public static class SignalrExtension
    {
        public static void AddSignalRWithAddition(this IServiceCollection services)
        {
            services.AddSingleton<IUserIdProvider, SignalrUserIdProvider>();
            services.AddSignalR();
        }
    }
}
