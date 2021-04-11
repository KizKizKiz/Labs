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
        /// <summary>
        /// Конструктор итератора.
        /// </summary>
        /// <param name="graph">Граф.</param>
        public DFSIterator(Graph<TValue> graph)
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

                foreach (var value in _graph.Items.Single(c => c.Vertex.Equals(vertex)).Items.Select(c => c.Target))
                {
                    if (!mapVertexAndIsMarked[value])
                    {
                        WalkBasedOn(value);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly Graph<TValue> _graph;
    }
}