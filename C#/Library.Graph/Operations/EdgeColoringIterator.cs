using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет итератор реберной раскраски графа.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class EdgeColoringIterator<TValue> : IEnumerable<(EdgeItem<TValue>, int)>
        where TValue : notnull, IEqualityComparer<TValue>, IEquatable<TValue>
    {
        private class EdgeAndColor
        {
            public EdgeItem<TValue> Edge { get; }

            public int? Color { get; set; }

            public EdgeAndColor(EdgeItem<TValue> edge, int? color = default)
            {
                Edge = edge;
                Color = color;
            }
        }

        /// <summary>
        /// Конструктор итератора.
        /// </summary>
        /// <param name="graph">Граф.</param>
        public EdgeColoringIterator(Graph<TValue> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (graph.IsOriented)
            {
                throw new ArgumentException("Algorithm is required in a unoriented graph.", nameof(graph));
            }
            if (graph.ConnectivityType == Generators.Options.ConnectivityType.NotConnected)
            {
                throw new ArgumentException("Algorithm is required in a connected graph.", nameof(graph));
            }
            _graph = graph;

            _mapVertexAndEdgesWithColor = _graph.Items.ToDictionary(
                c => c.Key,
                c => c.Value.Items.Select(c => new EdgeAndColor(c)).ToHashSet());
        }

        /// <summary>
        /// Возвращает итератор реберной раскраски.
        /// </summary>
        public IEnumerator<(EdgeItem<TValue>, int)> GetEnumerator()
        {
            GiveColorToEdges();
            return _mapVertexAndEdgesWithColor
                .Values
                .Select(c => c.Select(v => (v.Edge, v.Color!.Value)))
                .SelectMany(c => c)
                .GetEnumerator();
        }

        public void GiveColorToEdges()
        {
            foreach (var edge in _graph.Edges)
            {
                var usedColors = _mapVertexAndEdgesWithColor[edge.Source]
                    .Where(c => !c.Edge.Equals(edge) && c.Color is not null)
                    .Select(c => c.Color!.Value)
                    .ToHashSet();

                usedColors.UnionWith(_mapVertexAndEdgesWithColor[edge.Target]
                    .Where(c => !c.Edge.Equals(BackEdge(edge)) && c.Color is not null)
                    .Select(c => c.Color!.Value));

                AssignColor(edge.Source, edge.Target, edge, GetColor(usedColors));
            }
        }

        private void AssignColor(TValue source, TValue target, EdgeItem<TValue> edge, int color)
        {
            var sourceEdgeAndColor = _mapVertexAndEdgesWithColor[source].Single(e => e.Edge.Equals(edge));
            sourceEdgeAndColor.Color = color;

            var targetEdgeAndColor = _mapVertexAndEdgesWithColor[target].Single(e => e.Edge.Equals(BackEdge(edge)));
            targetEdgeAndColor.Color = color;
        }

        private int GetColor(HashSet<int> usedColors)
        {
            var resultColor = -1;

            var pooledExceptUsed = _poolOfColors.Except(usedColors).ToList();
            if (!pooledExceptUsed.Any())
            {
                if (!_poolOfColors.Any())
                {
                    _poolOfColors.Add(1);
                }
                else
                {
                    _poolOfColors.Add(_poolOfColors[^1] + 1);
                }
                resultColor = _poolOfColors[^1];
            }
            else
            {
                resultColor = pooledExceptUsed[_random.Next(pooledExceptUsed.Count)];
            }
            return resultColor;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private EdgeItem<TValue> BackEdge(EdgeItem<TValue> edge)
            => edge.Weight is not null ?
            new EdgeItem<TValue>(edge.Target, edge.Source, edge.Weight!.Value)
            : new EdgeItem<TValue>(edge.Target, edge.Source);

        private readonly Random _random = new((int)DateTime.Now.Ticks & 0x0000FFFF);
        private readonly Graph<TValue> _graph;
        private readonly List<int> _poolOfColors = new();
        private readonly Dictionary<TValue, HashSet<EdgeAndColor>> _mapVertexAndEdgesWithColor;
    }
}
