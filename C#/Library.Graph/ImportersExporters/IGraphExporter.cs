using System.Threading.Tasks;

using Library.Graph.Types;

namespace Library.Graph.ImportersExporters
{
    /// <summary>
    /// Представляет контракт экспорта графа.
    /// </summary>
    public interface IGraphExporter
    {
        /// <summary>
        /// Экспортирует граф типа <see cref="Graph{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="graph">Граф.</param>
        Task ExportAsync<TValue>(Graph<TValue> graph)
            where TValue : notnull, IStringConvertible<TValue>;
    }
}