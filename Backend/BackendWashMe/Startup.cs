using FluentValidation;
using UseCases.PipelineBehaviours;

namespace BackendWashMe
{
    using BackendWashMe.Extensions;
    using BackendWashMe.SignalRHubs;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Connections;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;
    using UseCases.Handlers.Authorization.Queries;

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
            var useCasesAssembly = typeof(AuthorizationQueryHandler).GetTypeInfo().Assembly;
            services.AddAutoMapper(useCasesAssembly);
            services.AddDatabase(Configuration);
            services.AddIdentity(Configuration);
            services.AddJwtAuthentication(Configuration);
            services.AddMediatR(useCasesAssembly);
            services.AddControllersWithViews();
            services.AddSignalRWithAddition();
            services.AddInfrastructure(Configuration);
            services.AddApplicationServices();
            services.AddCors();
            services.AddValidation(useCasesAssembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<WashMeHub>("/washme", options =>
                {
                    options.ApplicationMaxBufferSize = 64;
                    options.TransportMaxBufferSize = 64;
                    options.Transports = HttpTransportType.WebSockets;
                });
            });
        }
    }
}
