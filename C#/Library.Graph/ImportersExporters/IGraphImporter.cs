using System.IO;
using System.Threading.Tasks;

using Library.Graph.Types;

namespace Library.Graph.ImportersExporters
{
    /// <summary>
    /// Представляет контракт импорта графа.
    /// </summary>
    public interface IGraphImporter
    {
        /// <summary>
        /// Возвращает граф из <paramref name="stream"/>.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        /// <param name="stream">Поток чтения графа.</param>
        /// <returns>Граф типа <typeparamref name="TValue"/>.</returns>
        Task<Graph<TValue>> ImportAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, new();
    }
}