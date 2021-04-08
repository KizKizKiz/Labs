using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет реализацию обхода в глубину неориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class DFSIterator<TValue> : IEnumerable<TValue>
        where TValue : notnull, new()
    {
        public DFSIterator(AdjacensiesBasedGraph<TValue> graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        /// <summary>
        /// Возвращает итератор обхода вершин.
        /// </summary>
        public IEnumerator<TValue> GetEnumerator() => SetupIterator().GetEnumerator();
        private IEnumerable<TValue> SetupIterator()
        {
            var mapVertexAndIsMarked = _graph.Items.ToDictionary(kv => kv.Vertex, _ => false);
            var passedVertices = new List<TValue>();

            WalkBasedOn(_graph.Items[0].Vertex);

            return passedVertices;

            void WalkBasedOn(TValue vertex)
            {
                passedVertices.Add(vertex);
                mapVertexAndIsMarked[vertex] = true;

                foreach (var item in _graph.Items.First(c => c.Vertex.Equals(vertex)).Items)
                {
                    if (!mapVertexAndIsMarked[item])
                    {
                        WalkBasedOn(item);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly AdjacensiesBasedGraph<TValue> _graph;
    }
}