using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using System.Threading;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Library.ORM;

namespace DbIntegrationApp
{
    public class Startup : IHostedService
    {
        public Startup(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<Startup> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hostApplication = hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = Task.Run(() =>
            {
                Application.Run(new TableViewer());

                //Стопаем приложение, т.к. приложение закрыто.
                _hostApplication.StopApplication();
            },
            cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The application is stopped. Shutdown...");
            return Task.CompletedTask;
        }
        public static async Task InitializeAsync()
        {
            _host = Host.CreateDefaultBuilder()
               .ConfigureAppConfiguration((context, builder) => ConfigureApp(builder))
               .ConfigureLogging((context, builder) => ConfigureLogging(context, builder))
               .ConfigureServices((context, builder) => ConfigureServices(builder))
               .Build();

            await _host.RunAsync(_host.Services.GetService<IHostApplicationLifetime>()!.ApplicationStopping);
        }

        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
        {
            builder.ClearProviders();

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom
                .Configuration(context.Configuration);

            builder.AddSerilog(loggerConfiguration.CreateLogger());
        }

        private static void ConfigureServices(IServiceCollection builder)
        {
            //Должен быть первым
            builder.AddHostedService<Startup>();

            builder.AddTransient<TableViewer>();
            builder.AddDbContext<Context>((Microsoft.EntityFrameworkCore.DbContextOptionsBuilder builder) => builder.UseSqlServer)
        }
        private static void ConfigureApp(IConfigurationBuilder builder)
        {
            builder.AddJsonFile("appsettings.json");
        }

        private readonly ILogger<Startup> _logger;
        private readonly IHostApplicationLifetime _hostApplication;
        private static IHost _host = null!;
    }
}