using ApplicationServices;
using ApplicationServices.Implementation;
using ApplicationServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BackendWashMe.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMessageReceiversCreatorService, MessageReceiversCreatorService>();
            services.AddScoped<IFreeTimeSlotService, FreeTimeSlotService>();
        }
    }
}
