using System;
using System.IO;
using FPTeLabLibarry;
using FPTeLabLibarry.DataAccess;
using FPTeLabLibarry.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.ServiceProcess;
using NLog.Web;
using System.Text;

namespace FPTeLabService
{
    public class Program
    {
        static SettingsConfig settings;
        //static ILogger logger;
        private static IConfiguration _Configuration;
        static ISettings _settings;
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var logger = NLogBuilder.ConfigureNLog("Log.config").GetCurrentClassLogger();
            bool IsService = Console.IsErrorRedirected;

            settings = new SettingsConfig();
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            _Configuration = builder.Build();
            var SettingsSection = _Configuration.GetSection("Settings");
            SettingsSection.Bind(settings);

            _settings = settings.ReSource;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ISettings>((settings.ReSource));
                    services.AddSingleton<BackgroundTaskQueueOther>();
                    services.AddSingleton<BackgroundTaskQueueResult>();
                    services.AddSingleton<Connection>(new XMLStatement(_settings, null));
                    services.AddSingleton<ConnectioneLab>(new XMLStatementeLab(_settings, null));
                    services.AddHostedService<QueuedHostedServiceOther>();
                    services.AddHostedService<QueuedHostedServiceResult>();
                    if (settings.ReSource.ConnectHis_v2)
                    {
                        services.AddHostedService<GetDataOrderHis_v2>();
                        services.AddHostedService<ReturnDataHis_v2>();
                    }
                    else
                    {
                        services.AddHostedService<GetDataOrderHis_v1>();
                        services.AddHostedService<GetResultDataeLab>();
                    }
                }).UseNLog().UseWindowsService();
            if (!IsService)
            {
                Console.WriteLine("Worker service is starting....");
                Console.CancelKeyPress += (sender, e) =>
                {
                    Console.WriteLine("Worker service is stopping...");
                    host.Build().StopAsync();
                    e.Cancel = true;
                };
                Console.WriteLine("Press CTRL+C to stop...");
                host.Build().Run();
            }
            else
                host.Build().Start();
        }
    }
}
