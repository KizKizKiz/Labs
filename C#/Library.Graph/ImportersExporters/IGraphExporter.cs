using System.Threading.Tasks;

using Library.Graph.Views;

namespace Library.Graph.ImportersExporters
{
    /// <summary>
    /// Представляет контракт представления графа.
    /// </summary>
    public interface IGraphExporter
    {
        /// <summary>
        /// Экспортирует представление типа <typeparamref name="TView"/>
        /// </summary>
        /// <param name="data">Представление для экспорта.</param>
        Task ExportAsync<TView, TViewItem, TValue>(TView view)
            where TView : IGraphView<TViewItem, TValue>
            where TViewItem : IGraphViewItem<TValue>
            where TValue : notnull;
    }
}