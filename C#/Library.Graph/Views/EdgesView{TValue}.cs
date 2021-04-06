using System;
using System.Collections.Generic;

namespace Library.Graph.Views
{
    public sealed class EdgesView<TValue> : GraphView<EdgesViewItem<TValue>, TValue>
    {
        public bool IsWeighted { get; }

        public EdgesView(IEnumerable<EdgesViewItem<TValue>> items, IEnumerable<TValue> vertices, bool isWeighted)
            : base(items, vertices) 
        {
            IsWeighted = isWeighted;
        }
    }
}