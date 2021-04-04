
using Library.Graph.Views;

namespace Library.Graph.Generators
{
    public sealed class AdjacenciesViewGeneratingResult<TValue> : ViewGeneratingResult<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>>
        where TValue : notnull
    {
        public AdjacenciesViewGeneratingResult(AdjacensiesView<TValue> view)
            : base(view)
        { }
    }
}