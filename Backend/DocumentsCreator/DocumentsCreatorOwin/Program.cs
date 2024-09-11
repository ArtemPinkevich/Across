using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace DocumentsCreatorOwin
{
    internal class Program
    {
        public static void Main(string[] args)
        {
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