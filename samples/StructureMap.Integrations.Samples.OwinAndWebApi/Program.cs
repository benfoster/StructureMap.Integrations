using System;

namespace StructureMap.Integrations.Samples.OwinAndWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9000";
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Web Server running on {0}. Press [enter] to quit...", url);
                Console.ReadLine();
            }
        }
    }
}
