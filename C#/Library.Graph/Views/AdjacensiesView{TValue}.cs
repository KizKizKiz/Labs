using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Views
{
    public class AdjacensiesView<TValue> : GraphView<AdjacensyViewItem<TValue>, TValue>
        where TValue: notnull
    {
        public AdjacensiesView(IEnumerable<AdjacensyViewItem<TValue>> adjacensies, IEnumerable<TValue> vertices)
            : base(adjacensies, vertices)
        {
            _mapVertexAndValues = adjacensies.ToDictionary(adj => adj.Vertex, adj => adj.Items);
        }

        public IEnumerable<TValue> GetValuesByVertex(TValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (!_mapVertexAndValues.TryGetValue(value, out var items))
            {
                throw new InvalidOperationException($"Vertex is not presented in map.({value})");
            }
            return items;
        }

        private readonly Dictionary<TValue, IReadOnlyList<TValue>> _mapVertexAndValues;
    }
}