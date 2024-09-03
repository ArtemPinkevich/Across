using System;
using DataAccess.BaseImplementation;
using DataAccess.BaseImplementation.Repositories;
using DataAccess.Interfaces;
using DataAccess.MySql;
using DataAccess.SqlLite;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Across.Extensions
{
    public static class DatabaseExtension
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string mysqlConnection = configuration.GetConnectionString("MySqlConnection");

            if (!bool.TryParse (configuration.GetSection("IsDevelopment").Value, out bool isDevelopment))
                isDevelopment = true;

            if (isDevelopment)
                services.AddDbContext<DatabaseContext, SqlLiteDbContext>(opt => opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                    .LogTo(Console.WriteLine, LogLevel.Debug));
            else
                services.AddDbContext<DatabaseContext, MySqlDbContext>(opt => opt.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));

            services.AddScoped<IRepository<Truck>, TrucksRepository>();
            services.AddScoped<IRepository<Cargo>, CargosRepository>();
            services.AddScoped<IRepository<TransportationOrder>, TransportationOrderRepository>();
            services.AddScoped<IRepository<TransportationStatusRecord>, TransportationStatusRecordRepository>();
            services.AddScoped<IRepository<DriverRequest>, DriverRequestRepository>();
            services.AddScoped<IRepository<Transportation>, TransportationRepository>();
        }
    }
}
