using System.Threading.Tasks;

using Library.Graph.Types;

namespace Library.Graph.ImportersExporters
{
    public interface IGraphExporter
    {
        Task ExportAsync<TValue>(AdjacensiesBasedGraph<TValue> graph)
            where TValue : notnull, IStringConvertible<TValue>;

        Task ExportAsync<TValue>(EdgesBasedGraph<TValue> graph)
            where TValue : notnull, IStringConvertible<TValue>;
    }
}