namespace BackendWashMe.Extensions
{
    using DataAccess.BaseImplementation;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using BackendWashMe.IdentityTokenProviders;

    public static class IdentityExtension
    {
        public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddSignInManager<SignInManager<User>>();

                if (!bool.TryParse (configuration.GetSection("SmsGateway:UseRealGateway").Value, out bool useRealSmsGateway))
                    useRealSmsGateway = false;
                
                if (useRealSmsGateway)
                    builder.AddDefaultTokenProviders();
                else
                    builder.AddCrutchTokenProviders();
        }
    }
}
