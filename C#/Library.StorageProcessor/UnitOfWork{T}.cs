using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Library.StorageProcessor.Model;

namespace Library.StorageProcessor
{
    public class UnitOfWork<T> : IUnitOfWork<T>
        where T : class, IIdentifable
    {
        public UnitOfWork(
            Context context,
            ILogger<T> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAsync(T entity, CancellationToken token = default)
        {
            await _context.Set<T>().AddAsync(entity, token);
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return Task.FromResult<IEnumerable<T>>(_context.Set<T>().Local);
        }

        public Task<T> GetByIdAsync(T id, CancellationToken token = default)
        {
            return Task.FromResult(_context.Set<T>().Find(id.ID));
        }

        public async Task LoadAsync(CancellationToken token = default)
        {
            await _context.Set<T>().LoadAsync();

            _logger.LogInformation("Loaded ({TypeName})", typeof(T).Name);
        }

        public Task RemoveAsync(T entity, CancellationToken token = default)
        {
            return Task.FromResult(_context.Set<T>().Remove(entity));
        }

        private readonly Context _context;
        private readonly ILogger<T> _logger;
    }
}
