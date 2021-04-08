using System.Collections.Generic;

using Library.Graph.Generators.Options;

namespace Library.Graph.Types
{
    public class TransportNetworkGraph<TValue> : AdjacensiesBasedGraph<TValue>
        where TValue : notnull
    {
        public TransportNetworkGraph(
            IEnumerable<AdjacensyGraphItem<TValue>> adjacensies,
            IEnumerable<TValue> vertices)
            : base(adjacensies, vertices, true, ConnectivityType.WeaklyOrJustConnected)
        { }
    }
}