//using System;
//using System.Collections;
//using System.Collections.Generic;

//using Library.GraphTypes.Views;

//namespace Library.GraphTypes.Operations
//{
//    public sealed class MinimumSpanningTreeIterator<TView, TValue> : IEnumerable<TValue>
        
//        where TView : EdgesWithWeightView<TValue>
//    {
//        public MinimumSpanningTreeIterator(Graph<TView, TValue> graph)
//        {
//            _graph = graph;
//        }

//        public IEnumerator<TValue> GetEnumerator()
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

//        private Graph<TView, TValue> _graph;
//    }
//}