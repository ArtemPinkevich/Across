using System.Reflection;
using Across.Extensions;
using Across.SignalRHubs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            var useCasesAssembly = typeof(AuthorizationQueryHandler).GetTypeInfo().Assembly;
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
            services.AddSwaggerGen();
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
