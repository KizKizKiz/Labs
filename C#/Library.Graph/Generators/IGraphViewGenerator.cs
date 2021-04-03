
using Library.Graph.Views;

namespace Library.Graph.Generators
{
    public interface IGraphViewGenerator<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        ViewGeneratingResult<TView, TViewItem, TValue> Build();
    }
}
