//using System;
//using System.Collections;
//using System.Collections.Generic;

//using Library.GraphTypes.Views;

//namespace Library.GraphTypes.Operations
//{
//    public class BFSIterator<TView, TValue> : IEnumerable<TValue>
        
//        where TView : GraphView<TView, TValue>
//    {
//        public BFSIterator(Graph<TView, TValue> graph)
//        {
//            _graph = graph;
//        }

//        public IEnumerator<TValue> GetEnumerator()
//        {
//            throw new NotImplementedException();
//        }

//        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

//        private readonly Graph<TView, TValue> _graph;
//    }
//}