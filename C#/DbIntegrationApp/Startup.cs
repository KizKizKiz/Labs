using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using System.Threading;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using NHibernate;

using Library.StorageProcessor;
using Library.Model;
using Library.StorageProcessor.EFAccessor;
//using Library.StorageProcessor.NHibernateAccessor;

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
                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();
                Application.ThreadException += (sen, info) => _logger.LogError(info.Exception, "Unhandled error occured. See info in details.");

                Application.Run(new TableViewer(_host.Services.GetService<ModelsContainer>()!));

                //Стопаем сервисы, т.к. приложение закрыто.
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
            builder.AddHostedService<Startup>();

            builder.AddSingleton<ModelsContainer>();
            builder.AddSingleton<IRepository<GoodType>, Repository<GoodType>>();
            builder.AddSingleton<IRepository<Good>, Repository<Good>>();
            builder.AddSingleton<IRepository<Provider>, Repository<Provider>>();
            builder.AddSingleton<IRepository<GoodProvider>, Repository<GoodProvider>>();
            builder.AddSingleton<INHibernateLoggerFactory, LoggerFactoryAdapter>();
            builder.AddHostedService<ModelContainerService<ModelsContainer>>();

            builder.AddSingleton<Context>();

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