using System.Threading.Tasks;

using Library.Graph.Views;

namespace Library.Graph.ImportersExporters
{
    /// <summary>
    /// Представляет контракт импорта представления графа.
    /// </summary>
    public interface IGraphViewImporter
    {
        /// <summary>
        /// Импортирует представление типа <typeparamref name="TView"/>.
        /// </summary>
        Task<TView> ImportAsync<TView, TViewItem, TValue>()
            where TView : IGraphView<TViewItem, TValue>
            where TViewItem : IGraphViewItem<TValue>
            where TValue : notnull;
    }
}