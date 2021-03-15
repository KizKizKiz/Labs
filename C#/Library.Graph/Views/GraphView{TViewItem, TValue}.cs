using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Views
{
    public abstract class GraphView<TViewItem, TValue> : IGraphView<TViewItem, TValue>
        where TValue : IEquatable<TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        public IReadOnlyList<TViewItem> Items { get; }

        public GraphView(IEnumerable<TViewItem> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Items = items.ToList();
        }
    }
}