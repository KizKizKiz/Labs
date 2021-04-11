using System;
using System.Collections;
using System.Collections.Generic;

using Library.Graph.Structures;
using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет итератор поиска минимального остовного дерева.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public sealed class MinimumSpanningTreeIterator<TValue> : IEnumerable<EdgeItem<TValue>>
         where TValue : notnull, new()
    {
        public MinimumSpanningTreeIterator(Graph<TValue> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.IsOriented)
            {
                throw new ArgumentException("The algorithm support adjacensies based 'oriented' graph.", nameof(graph));
            }
            _graph = graph;
        }

        /// <summary>
        /// Возвращает итератор поиска минимального остовного дерева.
        /// </summary>
        public IEnumerator<EdgeItem<TValue>> GetEnumerator()
            => SetupIterator().GetEnumerator();

        private IEnumerable<EdgeItem<TValue>> SetupIterator()
        {
            var pq = new MinPriorityQueue<EdgeItem<TValue>>(_graph.Edges);
            var uf = new UnionFindStructure<TValue>(_graph.Vertices);
            var mstCount = 0;
            while (!pq.IsEmpty() && mstCount < _graph.Vertices.Count - 1)
            {
                var edge = pq.DeleteMin();
                var v = edge.Source;
                var w = edge.Target ?? throw new InvalidOperationException("Received invalid view item, cause a second vertice in edge is null.");

                if (uf.TryUnion(v, w))
                {
                    mstCount++;
                    yield return edge;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly Graph<TValue> _graph;
    }
}