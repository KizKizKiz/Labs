using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.ExampleConvertibleTypes;
using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет итератор поиска кратчайших путей графа.
    /// </summary>
    public class FloydWarshallIterator : IEnumerable<IEnumerable<IntConvertible>>
    {
        public FloydWarshallIterator(Graph<IntConvertible> graph)
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
                throw new ArgumentException("For Floyd Warshall's algorithm graph must be weighted.");
            }
            if (graph.Edges.Any(v => v.Weight < 0))
            {
                throw new ArgumentException("Negative weight is not valid.");
            }

            _v = graph.Vertices.Count;
            _graph = graph;
            _distTo = new double[_v, _v];
            _edgeTo = new EdgeItem<IntConvertible>[_v, _v];

            Initializing();
            FloydWarshall();
        }
        private void Initializing()
        {
            for (var v = 0; v < _v; v++)
            {
                for (var w = 0; w < _v; w++)
                {
                    _distTo[v, w] = double.MaxValue;
                }
            }
            for (var v = 0; v < _v; v++)
            {
                foreach (var e in _graph.Items[v].Items)
                {
                    _distTo[e.Source.Number, e.Target.Number] = e.Weight!.Value;
                    _edgeTo[e.Source.Number, e.Target.Number] = e;
                }
                if (_distTo[v, v] >= 0.0)
                {
                    _distTo[v, v] = 0.0;
                    _edgeTo[v, v] = null!;
                }
            }
        }
        private bool HasPath(int s, int t) => _distTo[s, t] < double.MaxValue;

        private void FloydWarshall()
        {
            for (var i = 0; i < _v; i++)
            {
                for (var v = 0; v < _v; v++)
                {
                    if (_edgeTo[v, i] is null)
                    {
                        continue;
                    }
                    for (var w = 0; w < _v; w++)
                    {
                        if (_distTo[v, w] > _distTo[v, i] + _distTo[i, w])
                        {
                            _distTo[v, w] = _distTo[v, i] + _distTo[i, w];
                            _edgeTo[v, w] = _edgeTo[i, w];
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Возвращает итератор поиска кратчайшего пути от одной вершины к другой.
        /// </summary>
        /// <returns>Последовательность вершин, входящих в кратчайший путь.</returns>
        public IEnumerable<IntConvertible> GetIteratorFromSourceToTarget(int s, int t)
        {
            var verticesSet = new HashSet<IntConvertible>();
            var vertices = new List<IntConvertible>();
            if (HasPath(s, t))
            {
                for (var e = _edgeTo[s, t]; e != null; e = _edgeTo[s, e.Source.Number])
                {
                    if (verticesSet.Add(e.Target))
                    {
                        vertices.Add(e.Target);
                    }
                    if (verticesSet.Add(e.Source))
                    {
                        vertices.Add(e.Source);
                    }
                }
                vertices.Reverse();
                foreach (var v in vertices)
                {
                    yield return v;
                }
            }
        }
        /// <summary>
        /// Возвращает итератор поиска всех кратчайших путей.
        /// </summary>
        /// <returns>Последовательность от последовательности вершин, входящих в кратчайший путь.</returns>
        public IEnumerator<IEnumerable<IntConvertible>> GetEnumerator()
        {
            for (var v = 0; v < _v; v++)
            {
                for (var w = 0; w < _v; w++)
                {
                    yield return GetIteratorFromSourceToTarget(v, w);
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly double[,] _distTo;
        private readonly int _v;
        private readonly Graph<IntConvertible> _graph;
        private readonly EdgeItem<IntConvertible>[,] _edgeTo;
    }
}
