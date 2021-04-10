using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Generators.Options;

namespace Library.Graph.Types
{
    public class TransportNetworkGraph<TValue> : AdjacensiesBasedGraph<TValue>
        where TValue : notnull
    {
        public TValue Source { get; private set; } = default!;

        public TValue Target { get; private set; } = default!;

        public TransportNetworkGraph(
            IEnumerable<AdjacensyGraphItem<TValue>> adjacensies,
            IEnumerable<TValue> vertices,
            bool needToValidateConnectivityType = false)
            : base(adjacensies, vertices, true, ConnectivityType.WeaklyOrJustConnected, needToValidateConnectivityType)
        {
            ValidateItemsAndSetSourceAndTarget();
        }

        private void ValidateItemsAndSetSourceAndTarget()
        {
            var targets = Items.Where(c => !c.Items.Any()).ToHashSet();
            if (targets.Count != 1)
            {
                throw new InvalidOperationException($"The algorithm support transport network graph with only one 'TARGET' (Detected: {targets.Count})");
            }
            var vertices = Items.Select(c => c.Items).SelectMany(c => c).ToHashSet();
            if (vertices.Count != Vertices.Count - 1)
            {
                throw new InvalidOperationException($"The algorithm support transport network graph with only one 'SOURCE' (Detected: {vertices.Count})");
            }

            Target = targets.Single().Vertex;
            Source = Vertices.Except(vertices).Single();
        }
    }
}