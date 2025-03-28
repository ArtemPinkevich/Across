using System.Reflection;
using Across.Extensions;
using Across.SignalRHubs;
using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UseCases.Handlers.Authorization.Queries;

namespace Across
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var useCasesAssembly = typeof(UpdateAccessTokenQueryHandler).GetTypeInfo().Assembly;
            services.AddAutoMapper(useCasesAssembly);
            services.AddDatabase(Configuration);
            services.AddIdentity(Configuration);
            services.AddJwtAuthentication(Configuration);
            services.AddMediatR(useCasesAssembly);
            services.AddControllersWithViews();
            services.AddSignalRWithAddition();
            services.AddInfrastructure(Configuration);
            services.AddCors();
            services.AddValidation(useCasesAssembly);
            services.AddBackgroundJobs(Configuration);
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"{token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="oauth2"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandlerMiddleware();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHangfireDashboard();

            app.UseRouting();

            app.UseCors(builder => builder//.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .SetIsOriginAllowed(_ => true)
                                          .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<WashMeHub>("/across", options =>
                {
                    options.ApplicationMaxBufferSize = 64;
                    options.TransportMaxBufferSize = 64;
                    options.Transports = HttpTransportType.WebSockets;
                });
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
