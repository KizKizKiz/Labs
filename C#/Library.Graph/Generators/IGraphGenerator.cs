using Library.Graph.Types;

namespace Library.Graph.Generators
{
    public interface IGraphGenerator<TGraph, TViewItem, TValue>
        where TValue : notnull
        where TViewItem : IGraphViewItem<TValue>
        where TGraph : IGraph<TViewItem, TValue>
    {
        GraphGeneratingResult<TGraph, TViewItem, TValue> Generate();
    }
}
