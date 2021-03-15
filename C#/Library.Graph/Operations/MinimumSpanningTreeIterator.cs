using System;
using System.Collections;
using System.Collections.Generic;

using Library.Graph.Types;
using Library.Graph.Views;
using Library.Graph;
using Library.Graph.ConvertibleTypes;

namespace Library.Graph.Operations
{
   public sealed class MinimumSpanningTreeIterator<TValue> : IEnumerable<EdgeViewItemWithWeight<TValue>>
        where TValue : IEquatable<TValue>, IStringConvertible<TValue>, new()
   {
       public MinimumSpanningTreeIterator(OrientedEdgeWithWeightGraph<TValue> graph)
       {
           _graph = graph ?? throw new ArgumentNullException(nameof(graph));
       }

       public IEnumerator<EdgeViewItemWithWeight<TValue>> GetEnumerator()
           => SetupIterator().GetEnumerator();

       private IEnumerable<EdgeViewItemWithWeight<TValue>> SetupIterator()
       {
           var pq = new MinPrimaryQueue<EdgeViewItemWithWeight<TValue>>(_graph.View.Items);
           var uf = new UnionFindStructure<TValue>(_graph.Vertices);
           var mstCount = 0;
           while (!pq.IsEmpty() && mstCount < _graph.Vertices.Count - 1)
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