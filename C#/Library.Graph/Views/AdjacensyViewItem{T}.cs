using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.GraphTypes.Views
{
    public readonly struct AdjacensyViewItem<TValue> : IGraphViewItem<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public TValue Vertex { get; }
        public IReadOnlyList<TValue> Items { get; }
        public AdjacensyViewItem(TValue vertex, IEnumerable<TValue> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            Vertex = vertex ?? throw new ArgumentNullException(nameof(vertex));
            Items = items.ToList();
        }
    }
}