using System.IO;
using System.Threading.Tasks;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Views;

namespace Library.Graph.ImportersExporters
{
    public interface IGraphViewImporter
    {
        Task<AdjacensiesView<TValue>> ImportAdjacensiesViewAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, new();

        Task<EdgesView<TValue>> ImportEdgesViewAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, new();
    }
}