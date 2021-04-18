using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет итератор поиска кратчайшего пути графа.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class DijkstraShortestPathIterator<TValue> : IEnumerable<EdgeItem<TValue>>
        where TValue : notnull, IComparable<TValue>, IEqualityComparer<TValue>, IEquatable<TValue>
    {
        public DijkstraShortestPathIterator(TransportNetworkGraph<TValue> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The graph is empty.");
            }
            if (!graph.IsWeighted)
            {
                throw new ArgumentException("For Dijkstra algorithm graph must be weighted.");
            }
            if (graph.Edges.Any(v => v.Weight < 0))
            {
                throw new ArgumentException("Negative weight is not valid.");
            }
            _mapVertexAndDist = graph
                .Items
                .ToDictionary(
                kv => kv.Key,
                _ => double.MaxValue);

            _graph = graph;
            _edgeTo = new Dictionary<TValue, EdgeItem<TValue>>(graph.Vertices.Count);
            _pq = new SortedDictionary<double, TValue>();
        }

        /// <summary>
        /// Возвращает итератор поиска кратчайшего пути.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<EdgeItem<TValue>> GetEnumerator()
        {
            ShortestPath();
            if (HasPathTo(_graph.Target))
            {
                var e = _edgeTo[_graph.Target];
                var s = e.Source;
                while (!s.Equals(_graph.Source))
                {
                    var edge = e;
                    e = _edgeTo[s];
                    s = e.Source;
                    yield return edge;
                }
                if (s.Equals(_graph.Source))
                {
                    yield return e;
                }
            }
        }

        private void ShortestPath()
        {
            _mapVertexAndDist[_graph.Source] = 0.0;
            _pq.Add(_mapVertexAndDist[_graph.Source], _graph.Source);

            while (_pq.Count != 0)
            {
                var k = _pq.Keys.First();
                var v = _pq[k];
                _ = _pq.Remove(_pq.Keys.First());
                foreach (var e in _graph.Items[v].Items)
                {
                    Relax(e);
                }
            }
        }

        private void Relax(EdgeItem<TValue> e)
        {
            var v = e.Source;
            var w = e.Target;
            if (_mapVertexAndDist[w] > _mapVertexAndDist[v] + e.Weight)
            {
                _mapVertexAndDist[w] = _mapVertexAndDist[v] + e.Weight!.Value;
                _edgeTo[w] = e;
                if (_pq.ContainsKey(_mapVertexAndDist[w]))
                {
                    _ = _pq.Remove(_mapVertexAndDist[w]);
                }
                _pq.Add(_mapVertexAndDist[w], w);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private bool HasPathTo(TValue value) => _mapVertexAndDist[value] < double.MaxValue;

        private readonly TransportNetworkGraph<TValue> _graph;
        private readonly Dictionary<TValue, EdgeItem<TValue>> _edgeTo;
        private readonly SortedDictionary<double, TValue> _pq;
        private readonly Dictionary<TValue, double> _mapVertexAndDist;
    }
}
