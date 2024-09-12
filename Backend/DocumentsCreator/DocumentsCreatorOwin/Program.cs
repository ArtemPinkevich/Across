using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.Hosting;
using DbContext = DocumentsCreatorOwin.DAL.DbContext;

namespace DocumentsCreatorOwin
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();

            var options = optionsBuilder
                .UseSqlite(@"Filename=../../../../Across/Across.db")
                .Options;
            using (DbContext dbContext = new DbContext(options))
            {
                var orders = dbContext.TransportationOrders.ToList();
            }
            Console.WriteLine("Documents creator server starting");
            
            string baseAddress = "http://localhost:9000/"; 

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Documents creator server running");
                Console.ReadLine();
            } 
        }
    }
}