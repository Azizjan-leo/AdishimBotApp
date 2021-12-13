global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using Telegram.Bot;
global using Telegram.Bot.Types;
global using Telegram.Bot.Types.Enums;
global using AdishimBotApp.Models;
global using AdishimBotApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace AdishimBotApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var cts = new CancellationTokenSource();

            //var host = CreateHostBuilder(args).Build();

            //CreateDbIfNotExists(host);
                  
            Bot.Start(cts);    
            CreateHostBuilder(args).Build().Run();
            //host.Run();
        }

        //private static void CreateDbIfNotExists(IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        try
        //        {
        //            var context = services.GetRequiredService<ApplicationDbContext>();
        //        }
        //        catch (Exception ex)
        //        {
        //            var logger = services.GetRequiredService<ILogger<Program>>();
        //            logger.LogError(ex, "An error occurred creating the DB.");
        //        }
        //    }
        //}


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
