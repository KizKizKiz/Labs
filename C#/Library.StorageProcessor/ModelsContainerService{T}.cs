using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace Library.StorageProcessor
{
    public class ModelContainerService<T> : IHostedService
        where T : IHostedService
    {
        public ModelContainerService(T service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _service.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _service.StopAsync(cancellationToken);
        }

        private readonly T _service;
    }
}
