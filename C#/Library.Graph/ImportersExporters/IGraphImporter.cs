using System.IO;
using System.Threading.Tasks;

using Library.Graph.Types;

namespace Library.Graph.ImportersExporters
{
    public interface IGraphImporter
    {
        Task<AdjacensiesBasedGraph<TValue>> ImportAdjacensiesViewAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, new();

        Task<EdgesBasedGraph<TValue>> ImportEdgesViewAsync<TValue>(Stream stream)
            where TValue : notnull, IStringConvertible<TValue>, new();
    }
}