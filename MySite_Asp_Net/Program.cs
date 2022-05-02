using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MySite_Asp_Net
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();
            using(var s = host.Services.CreateScope())
            {
                var services = s.ServiceProvider;
                try
                {
                    var cont = services.GetRequiredService<StorageContext>();   // ������� ��� ��� ������ � ��.
                    DefaultData.Initialize(cont);
                    
                }
                catch(Exception ex)
                {
                    var log = services.GetRequiredService<ILogger<Program>>();         // ����� ��������� ��� ������ ������� � ����� Program.
                    log.LogError(ex.Message, "Error with Db");
                }
            }
            host.Run();             // ������� ��������.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
