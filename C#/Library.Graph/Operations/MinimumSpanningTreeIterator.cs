using System;
using System.Collections;
using System.Collections.Generic;

using Library.Graph.Structures;
using Library.Graph.Types;

namespace Library.Graph.Operations
{
    public sealed class MinimumSpanningTreeIterator<TValue> : IEnumerable<EdgesViewItem<TValue>>
         where TValue : notnull, new()
    {
        public MinimumSpanningTreeIterator(EdgesBasedGraph<TValue> graph)
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

        public IEnumerator<EdgesViewItem<TValue>> GetEnumerator()
            => SetupIterator().GetEnumerator();

        private IEnumerable<EdgesViewItem<TValue>> SetupIterator()
        {
            var pq = new MinPrimaryQueue<EdgesViewItem<TValue>>(_graph.Items);
            var uf = new UnionFindStructure<TValue>(_graph.Vertices);
            var mstCount = 0;
            while (!pq.IsEmpty() && mstCount < _graph.Vertices.Count - 1)
            {
                var edge = pq.DeleteMin();
                var v = edge.First;
                var w = edge.Second ?? throw new InvalidOperationException("Received invalid view item, cause a second vertice in edge is null.");
                
                if (uf.TryUnion(v, w))
                {
                    mstCount++;
                    yield return edge;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly EdgesBasedGraph<TValue> _graph;
    }
}