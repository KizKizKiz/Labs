using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Library.StorageProcessor.Model;

namespace Library.StorageProcessor
{
    public interface IUnitOfWork<T> 
        where T: IIdentifable
    {
        Task AddAsync(T entity, CancellationToken token = default);

        Task RemoveAsync(T entity, CancellationToken token = default);

        Task<T> GetByIdAsync(T id, CancellationToken token = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);

        Task LoadAsync(CancellationToken token = default);
    }
}
