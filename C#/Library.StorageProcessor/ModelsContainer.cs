using System;
using System.Threading.Tasks;
using System.Threading;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

using Library.Model;

namespace Library.StorageProcessor
{
    public sealed class ModelsContainer : IHostedService
    {
        public IRepository<Good> GoodsRepository { get; }
        
        public IRepository<GoodType> GoodTypesRepository { get; }
        
        public IRepository<Provider> ProvidersRepository { get; }
        
        public IRepository<GoodProvider> GoodsProvidersRepository { get; }

        public Func<Task> EntitiesLoaded { get; set; } = null!;

        public ModelsContainer(
            ILogger<ModelsContainer> logger,
            IRepository<Good> goodsWork,
            IRepository<GoodType> goodTypesWork,
            IRepository<Provider> providerWork,
            IRepository<GoodProvider> goodsProvidersWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            GoodsRepository = goodsWork ?? throw new ArgumentNullException(nameof(goodsWork));
            GoodTypesRepository = goodTypesWork ?? throw new ArgumentNullException(nameof(goodTypesWork));
            ProvidersRepository = providerWork ?? throw new ArgumentNullException(nameof(providerWork));
            GoodsProvidersRepository = goodsProvidersWork ?? throw new ArgumentNullException(nameof(goodsProvidersWork));
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

            await GoodsRepository.LoadAsync(token);
            await GoodTypesRepository.LoadAsync(token);
            await GoodsProvidersRepository.LoadAsync(token);
            await ProvidersRepository.LoadAsync(token);

            EntitiesLoaded?.Invoke();

            _logger.LogInformation("Cached successfully.");
        }

        private readonly ILogger<ModelsContainer> _logger;
        private Task? _loadTask;
    }
}
