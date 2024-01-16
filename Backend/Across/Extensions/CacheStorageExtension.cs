using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Across.Extensions;

public static class CacheStorageExtension
{
    public static void AddCacheStorage(this IServiceCollection services, IConfiguration configuration)
    {
        if (!bool.TryParse (configuration["Redis:UseRealRedis"], out bool userRealRedis))
            userRealRedis = false;
        
        if (userRealRedis)
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "";
            });
        else
            services.AddDistributedMemoryCache();

    }
}