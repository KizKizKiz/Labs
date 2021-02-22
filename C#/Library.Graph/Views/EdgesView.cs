using System;
using System.Collections.Generic;

namespace Library.GraphTypes.Views
{
    public abstract class EdgesView<TViewItem, TValue> : GraphView<TViewItem, TValue>
        where TViewItem : IEdgeViewItem<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public EdgesView(IEnumerable<TViewItem> items)
            :base(items) { }
    }
}