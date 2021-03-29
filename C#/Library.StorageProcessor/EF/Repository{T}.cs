using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Library.Model;

namespace Library.StorageProcessor.EFAccessor
{
    public sealed class Repository<T> : IRepository<T>
        where T : class, IIdentifable
    {
        public Repository(
            Context context,
            ILogger<T> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAsync(T entity, CancellationToken token = default)
        {
            await _context.Set<T>().AddAsync(entity, token).ConfigureAwait(false);
            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return Task.FromResult<IEnumerable<T>>(_context.Set<T>().Local);
        }

        public Task<T> GetByIdAsync(int id, CancellationToken token = default)
        {
            return Task.FromResult(_context.Set<T>().Find(id));
        }

        public async Task LoadAsync(CancellationToken token = default)
        {
            await _context.Set<T>().LoadAsync();

            _logger.LogInformation("Loaded ({TypeName})", typeof(T).Name);
        }

        public async Task RemoveAsync(T entity, CancellationToken token = default)
        {
            _ = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> expression, CancellationToken token = default)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> expression, CancellationToken token = default)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            return await _context.Set<T>().Select(expression).ToListAsync();
        }

        public Task<IEnumerable<TResult>> GetAllCachedAsync<TResult>(Func<T, TResult> selector, CancellationToken token = default)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            return Task.FromResult(_context.Set<T>().Local.Select(selector));
        }

        public Task<IEnumerable<T>> GetAllCachedAsync(CancellationToken token = default)
        {
            return Task.FromResult(_context.Set<T>().Local.AsEnumerable());
        }

        private readonly Context _context;
        private readonly ILogger<T> _logger;
    }
}
