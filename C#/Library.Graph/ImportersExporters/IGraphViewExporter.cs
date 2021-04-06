using System.Threading.Tasks;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Views;

namespace Library.Graph.ImportersExporters
{
    public interface IGraphViewExporter
    {
        Task ExportAsync<TValue>(AdjacensiesView<TValue> view, bool isOriented)
            where TValue : notnull, IStringConvertible<TValue>;

        Task ExportAsync<TValue>(EdgesView<TValue> view, bool isOriented)
            where TValue : notnull, IStringConvertible<TValue>;
    }
}