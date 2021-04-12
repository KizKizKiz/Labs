using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет калькулятор вычисления величины максимального потока.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class MaxFlowCalculator<TValue>
        where TValue : notnull
    {
        private class FlowCapacityEdge
        {
            public TValue To { get; }

            public double Flow { get; set; }

            public double Capacity { get; }

            public TValue Reverse { get; }

            public FlowCapacityEdge(TValue to, double flow, double capacity, TValue reverse)
            {
                To = to;
                Flow = flow;
                Capacity = capacity;
                Reverse = reverse;
            }
        }

        /// <summary>
        /// Конструктор калькулятора.
        /// </summary>
        /// <param name="graph">Граф.</param>
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
            _mapVertexAndEdge = _graph.Items.ToDictionary(c => c.Key, (c) => new List<FlowCapacityEdge>());
            _mapVertexAndLevel = _graph.Items.ToDictionary(c => c.Key, (_) => -1);

            foreach (var item in _graph.Edges)
            {
                if (item.Source.Equals(_graph.Target))
                {
                    continue;
                }
                if (item.Target is not null)
                {
                    if (item.Weight!.Value < 0)
                    {
                        throw new ArgumentException("Algorithm cannot be executed on graph with negative weight.");
                    }
                    var forward = new FlowCapacityEdge(item.Target, 0, item.Weight!.Value, item.Source);
                    var backward = new FlowCapacityEdge(item.Source, 0, 0, item.Target);
                    _mapVertexAndEdge[forward.To].Add(backward);
                    _mapVertexAndEdge[backward.To].Add(forward);
                }
            }
        }

        /// <summary>
        /// Возвращает величину максимального потока.
        /// </summary>
        /// <returns></returns>
        public double Calculate()
        {
            var maxFlow = 0.0;

            while (CanSendMoreFlow())
            {
                var mapVertexAndVisitedVertices = _graph.Vertices.ToDictionary(c => c, _ => 0);
                while (true)
                {
                    var flow = SendFlow(_graph.Source, _graph.Target, int.MaxValue, mapVertexAndVisitedVertices);

                    if (flow <= 0)
                    {
                        break;
                    }

                    maxFlow += flow;
                }
            }

            return maxFlow;
        }

        private double SendFlow(TValue u, TValue target, double flow, Dictionary<TValue, int> map)
        {
            if (u.Equals(target))
            {
                return flow;
            }

            for (; map[u] < _mapVertexAndEdge[u].Count; map[u]++)
            {
                var e = _mapVertexAndEdge[u][map[u]];
                if (_mapVertexAndLevel[e.To] == _mapVertexAndLevel[u] + 1 && e.Flow < e.Capacity)
                {
                    var currentFlow = Math.Min(flow, e.Capacity - e.Flow);
                    var temporaryFlow = SendFlow(e.To, target, currentFlow, map);
                    if (temporaryFlow > 0)
                    {
                        e.Flow += temporaryFlow;
                        var reverse = _mapVertexAndEdge[e.To].First(c => c.To.Equals(e.Reverse));
                        reverse.Flow -= temporaryFlow;

                        return temporaryFlow;
                    }
                }
            }

            return 0;
        }

        private bool CanSendMoreFlow()
        {
            foreach (var keys in _mapVertexAndLevel.Keys)
            {
                _mapVertexAndLevel[keys] = -1;
            }
            _mapVertexAndLevel[_graph.Source] = 0;

            var vertices = new Queue<TValue>();
            vertices.Enqueue(_graph.Source);

            while (vertices.Any())
            {
                var u = vertices.Dequeue();

                foreach (var item in _mapVertexAndEdge[u])
                {
                    if (_mapVertexAndLevel[item.To] < 0 && item.Flow < item.Capacity)
                    {
                        _mapVertexAndLevel[item.To] = _mapVertexAndLevel[u] + 1;

                        vertices.Enqueue(item.To);
                    }
                }
            }
            return _mapVertexAndLevel[_graph.Target] >= 0;
        }

        private readonly TransportNetworkGraph<TValue> _graph;
        private readonly Dictionary<TValue, List<MaxFlowCalculator<TValue>.FlowCapacityEdge>> _mapVertexAndEdge;
        private readonly Dictionary<TValue, int> _mapVertexAndLevel;
    }
}