using System;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    public sealed class MaxFlowCalculator<TValue>
        where TValue : notnull
    {
        public MaxFlowCalculator(
            TransportNetworkGraph<TValue> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (graph.Items is null)
            {
                throw new ArgumentException("The graph items is null.", nameof(graph));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The graph items collection is empty.", nameof(graph));
            }
            _graph = graph;
        }

        public int Calculate() => 0;

        private readonly TransportNetworkGraph<TValue> _graph;
    }
}