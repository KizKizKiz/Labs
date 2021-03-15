using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Views
{
    public class OrientedAdjacensiesView<TValue> : GraphView<AdjacensyViewItem<TValue>, TValue>
        where TValue : IEquatable<TValue>
    {
        public OrientedAdjacensiesView(IEnumerable<AdjacensyViewItem<TValue>> adjacensies)
            : base(adjacensies)
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