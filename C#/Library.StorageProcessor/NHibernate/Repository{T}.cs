using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Library.Model;

namespace Library.StorageProcessor.NHibernateAccessor
{
    public sealed class Repository<T> : IRepository<T>
        where T: class, IIdentifable
    {
        public Repository(
            ILogger<T> logger,
            Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task AddAsync(T entity, CancellationToken token = default)
        {
            await _context.CurrentSession.SaveOrUpdateAsync(entity).ConfigureAwait(false);
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        {
            return Task.FromResult(_context.CurrentSession.Query<T>().AsEnumerable());
        }

        public Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> expression, CancellationToken token = default)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            return Task.FromResult(_context.CurrentSession.Query<T>().Select(expression).AsEnumerable());
        }

        public Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> expression, CancellationToken token = default)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            return Task.FromResult(_context.CurrentSession.Query<T>().Where(expression).AsEnumerable());
        }

        public Task<IEnumerable<TResult>> GetAllCachedAsync<TResult>(Func<T, TResult> selector, CancellationToken token = default)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            return Task.FromResult(_context.CurrentSession.Query<T>().Select(selector));
        }

        public async Task<IEnumerable<T>> GetAllCachedAsync(CancellationToken token = default)
        {
            return await _context.CurrentSession.QueryOver<T>().Cacheable().ListAsync(token).ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken token = default)
        {
            return await _context.CurrentSession.GetAsync<T>(id, token);
        }

        public async Task LoadAsync(CancellationToken token = default)
        {
            _ = await _context.CurrentSession.QueryOver<T>().Cacheable().ListAsync(token).ConfigureAwait(false);

            _logger.LogInformation("Loaded ({TypeName})", typeof(T).Name);
        }

        public async Task RemoveAsync(T entity, CancellationToken token = default)
        {
            using var transaction = _context.CurrentSession.BeginTransaction();
            transaction.Begin();
            try
            {
                await _context.CurrentSession.EvictAsync(entity, token).ConfigureAwait(false);
                await _context.CurrentSession.DeleteAsync(entity, token).ConfigureAwait(false);
                await _context.CurrentSession.FlushAsync(token).ConfigureAwait(false);
                await transaction.CommitAsync(token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        private readonly Context _context;
        private readonly ILogger<T> _logger;
    }
}
