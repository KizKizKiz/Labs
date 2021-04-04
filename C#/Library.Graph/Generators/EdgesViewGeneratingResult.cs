
using Library.Graph.Views;

namespace Library.Graph.Generators
{
    public sealed class EdgesViewGeneratingResult<TValue> : ViewGeneratingResult<EdgesWithWeightView<TValue>, EdgeViewItem<TValue>, TValue>>
        where TValue : notnull
    {
        public EdgesViewGeneratingResult(EdgesWithWeightView<TValue> view)
            : base(view)
        { }
    }
}