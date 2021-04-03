using System;
using System.Collections.Generic;

namespace Library.Graph.Views
{
    public sealed class EdgesWithWeightView<TValue> : GraphView<EdgeViewItem<TValue>, TValue>
    {
        public EdgesWithWeightView(IEnumerable<EdgeViewItem<TValue>> items, IEnumerable<TValue> vertices)
            : base(items, vertices) { }
    }
}