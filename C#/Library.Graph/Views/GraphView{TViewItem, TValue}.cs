using Library.Graph.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Views
{
    public abstract class GraphView<TViewItem, TValue> : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        public IReadOnlyList<TViewItem> Items { get; }

        public IReadOnlyList<TValue> Vertices { get; }

        public GraphView(IEnumerable<TViewItem> items, IEnumerable<TValue> vertices)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (vertices is null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (!items.Any())
            {
                throw new ArgumentException("The items collection is empty.", nameof(items));
            }
            if (!vertices.Any())
            {
                throw new ArgumentException("The vertices collection is empty.", nameof(vertices));
            }
            Vertices = vertices.ToList();
            Items = items.ToList();
        }
    }
}