using Infrastructure.Interfaces;
using Infrastructure.SmsGateway;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Across.Extensions
{
    public static class InfrastructureExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmsGatewayConfiuration>(configuration.GetSection("SmsGateway"));
            services.AddScoped<ISmsGateway, PilotSmsGateway>();
        }
    }
}
