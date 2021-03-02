using System;
using System.Collections;
using System.Collections.Generic;

using Library.GraphTypes;
using Library.Views;
using Library.Graph;

namespace Library.Operations
{
   public sealed class MinimumSpanningTreeIterator<TGraph, TValue> : IEnumerable<EdgeViewItemWithWeight<TValue>>
        where TGraph : EdgeWithWeightGraph<TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>, IStringConvertible<TValue>
   {
       public MinimumSpanningTreeIterator(TGraph graph)
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

       private readonly TGraph _graph;
   }
}
