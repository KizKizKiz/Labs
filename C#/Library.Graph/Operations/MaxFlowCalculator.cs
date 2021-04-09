using Library.Graph.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Graph.Operations
{
    public sealed class MaxFlowCalculator<TValue>
        where TValue : notnull
    {
        public MaxFlowCalculator(
            TransportNetworkGraph<TValue> graph,
            bool needToValidateTNGraph = false)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (graph.Items is null)
            {
                throw new ArgumentException("The graph items is null.", nameof(graph.Items));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The graph items collection is empty.", nameof(graph.Items));
            }
            _graph = graph;

            VerifyIfNeed(needToValidateTNGraph);
        }

        public int Calculate()
        {
            
        }

        private void VerifyIfNeed(bool needToValidateTNGraph)
        {
            if (needToValidateTNGraph)
            {
                VerifyTransportNetwork();
            }
        }

        private void VerifyTransportNetwork()
        {
            var targetCount = _graph.Items.Where(c => !c.Items.Any()).Count();
            if (targetCount != 1)
            {
                throw new InvalidOperationException($"The algorithm support transport network graph with only one 'TARGET' (Detected: {targetCount})");
            }
            var vertices = _graph.Items.Select(c => c.Items).SelectMany(c => c).ToHashSet();
            if (vertices.Count == _graph.Vertices.Count - 1)
            {
                throw new InvalidOperationException($"The algorithm support transport network graph with only one 'SOURCE' (Detected: {vertices.Count})");
            }
        }

        private readonly TransportNetworkGraph<TValue> _graph;
    }
}