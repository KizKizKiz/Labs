using System;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

using Library.StorageProcessor.Model;

namespace Library.StorageProcessor
{
    public sealed class ModelsContainer : IHostedService
    {
        public IUnitOfWork<Good> GoodsWorker { get; }
        
        public IUnitOfWork<GoodType> GoodTypesWorker { get; }
        
        public IUnitOfWork<Provider> ProvidersWorker { get; }
        
        public IUnitOfWork<GoodsProviders> GoodsProvidersWorker { get; }

        public Func<Task> EntitiesLoaded { get; set; } = null!;

        public ModelsContainer(
            ILogger<ModelsContainer> logger,
            IUnitOfWork<Good> goodsWork,
            IUnitOfWork<GoodType> goodTypesWork,
            IUnitOfWork<Provider> providerWork,
            IUnitOfWork<GoodsProviders> goodsProvidersWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            GoodsWorker = goodsWork ?? throw new ArgumentNullException(nameof(goodsWork));
            GoodTypesWorker = goodTypesWork ?? throw new ArgumentNullException(nameof(goodTypesWork));
            ProvidersWorker = providerWork ?? throw new ArgumentNullException(nameof(providerWork));
            GoodsProvidersWorker = goodsProvidersWork ?? throw new ArgumentNullException(nameof(goodsProvidersWork));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _logger.BeginScope("LOADING");

            _loadTask = Task.Run(async () => await LoadAsync(cancellationToken), cancellationToken);

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Model loader stopping...");

            if (_loadTask is not null)
            {
                await _loadTask;
            }

            _logger.LogInformation("Model loader stopped.");
        }

        private async Task LoadAsync(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            await GoodsWorker.LoadAsync(token);
            await GoodTypesWorker.LoadAsync(token);
            await GoodsProvidersWorker.LoadAsync(token);
            await ProvidersWorker.LoadAsync(token);

            EntitiesLoaded?.Invoke();

            _logger.LogInformation("Cached successfully.");
        }

        private readonly ILogger<ModelsContainer> _logger;
        private Task? _loadTask;
    }
}
