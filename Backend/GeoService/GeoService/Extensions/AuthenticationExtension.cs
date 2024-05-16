using System;
using System.Text;
using System.Threading.Tasks;
using GeoService.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GeoService.Extensions;

public static class AuthenticationExtension
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection("Jwt"));

        var issuer = configuration["Jwt:Issuer"];
        var audioence = configuration["Jwt:Audience"];
        var key = configuration["Jwt:Key"];

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidateIssuer = true,
                    ValidIssuer = issuer,
                    //ValidateAudience = true,
                    ValidAudience = audioence,
                    //ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuerSigningKey = true,
                    //ClockSkew = TimeSpan.Zero
                };
                
                /*
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/across")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                };*/
            });
    }
}
