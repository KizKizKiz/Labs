
using Library.Graph.Views;

namespace Library.Graph.Generators
{
    public interface IGraphViewGenerator<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TValue : notnull
    {
        ViewGeneratingResult<TView, TViewItem, TValue> Build();
    }
}
