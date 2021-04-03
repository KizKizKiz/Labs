using System;
using System.Collections;
using System.Collections.Generic;

using Library.Graph.Types;
using Library.Graph.Views;
using Library.Graph;
using Library.Graph.ConvertibleTypes;

namespace Library.Graph.Operations
{
   public sealed class MinimumSpanningTreeIterator<TValue> : IEnumerable<EdgeViewItem<TValue>>
        where TValue : IStringConvertible<TValue>, new()
   {
       public MinimumSpanningTreeIterator(OrientedEdgeWithWeightGraph<TValue> graph)
       {
           _graph = graph ?? throw new ArgumentNullException(nameof(graph));
       }

       public IEnumerator<EdgeViewItem<TValue>> GetEnumerator()
           => SetupIterator().GetEnumerator();

       private IEnumerable<EdgeViewItem<TValue>> SetupIterator()
       {
           var pq = new MinPrimaryQueue<EdgeViewItem<TValue>>(_graph.View.Items);
           var uf = new UnionFindStructure<TValue>(_graph.View.Vertices);
           var mstCount = 0;
           while (!pq.IsEmpty() && mstCount < _graph.View.Vertices.Count - 1)
           {
               var edge = pq.DeleteMin();
               var v = edge.First;
               var w = edge.Second;
               if (uf.TryUnion(v, w))
               {
                   mstCount++;
                   yield return edge;
               }
           }
       }

       IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

       private readonly OrientedEdgeWithWeightGraph<TValue> _graph;
   }
}