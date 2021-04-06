using System;
using System.Collections;
using System.Collections.Generic;

using Library.Graph.Types;
using Library.Graph.Views;
using Library.Graph;
using Library.Graph.ConvertibleTypes;
using Library.Graph.Types.Edges;

namespace Library.Graph.Operations
{
    public sealed class MinimumSpanningTreeIterator<TValue> : IEnumerable<EdgesViewItem<TValue>>
         where TValue : IStringConvertible<TValue>, new()
    {
        public MinimumSpanningTreeIterator(OrientedEdgesGraph<TValue> graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        public IEnumerator<EdgesViewItem<TValue>> GetEnumerator()
            => SetupIterator().GetEnumerator();

        private IEnumerable<EdgesViewItem<TValue>> SetupIterator()
        {
            var pq = new MinPrimaryQueue<EdgesViewItem<TValue>>(_graph.View.Items);
            var uf = new UnionFindStructure<TValue>(_graph.View.Vertices);
            var mstCount = 0;
            while (!pq.IsEmpty() && mstCount < _graph.View.Vertices.Count - 1)
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

        private readonly OrientedEdgesGraph<TValue> _graph;
    }
}