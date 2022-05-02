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
                    var cont = services.GetRequiredService<StorageContext>();   // підєднуємо щоб був звязок з БД.
                    DefaultData.Initialize(cont);
                    
                }
                catch(Exception ex)
                {
                    var log = services.GetRequiredService<ILogger<Program>>();         // файли логування для виводу помилок з класу Program.
                    log.LogError(ex.Message, "Error with Db");
                }
            }
            host.Run();             // запускає програму.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
