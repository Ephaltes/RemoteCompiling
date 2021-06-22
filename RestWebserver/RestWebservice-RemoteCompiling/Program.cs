using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace RestWebservice_RemoteCompiling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    var appsettingsPath = "appsettings.json";
                    var pathFromEnv = Environment.GetEnvironmentVariable("AppSettingsPath");

                    if (!string.IsNullOrEmpty(pathFromEnv))
                        appsettingsPath = pathFromEnv;

                    
                    configApp.AddJsonFile(appsettingsPath, optional: false);
                    configApp.AddCommandLine(args);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
    }
}
