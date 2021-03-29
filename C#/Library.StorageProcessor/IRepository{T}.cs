using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Library.Model;

namespace Library.StorageProcessor
{
    public interface IRepository<T>
        where T : IIdentifable
    {
        Task AddAsync(T entity, CancellationToken token = default);

        Task RemoveAsync(T entity, CancellationToken token = default);

        Task<T> GetByIdAsync(int id, CancellationToken token = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);

        Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> expression, CancellationToken token = default);

        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> expression, CancellationToken token = default);

        Task<IEnumerable<TResult>> GetAllCachedAsync<TResult>(Func<T, TResult> selector, CancellationToken token = default);

        Task<IEnumerable<T>> GetAllCachedAsync(CancellationToken token = default);

        Task LoadAsync(CancellationToken token = default);
    }
}
