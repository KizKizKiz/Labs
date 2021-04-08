using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет реализацию обхода в ширину неориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class BFSIterator<TValue> : IEnumerable<TValue>
         where TValue : notnull, new()
    {
        public BFSIterator(AdjacensiesBasedGraph<TValue> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The graph is empty.");
            }
            _mapVertexItems = graph
                .Items
                .ToDictionary(
                    item => item.Vertex,
                    item => item.Items);
        }

        /// <summary>
        /// Возвращает итератор обхода вершин.
        /// </summary>
        public IEnumerator<TValue> GetEnumerator() => SetupIterator().GetEnumerator();

        private IEnumerable<TValue> SetupIterator()
        {
            var mapVertexAndIsMarked = _mapVertexItems.ToDictionary(v => v.Key, _ => false);

            var verticesQueue = new Queue<TValue>();

            verticesQueue.Enqueue(_mapVertexItems.Keys.First());

            while (verticesQueue.Any())
            {
                var vertex = verticesQueue.Dequeue();

                yield return vertex;

                foreach (var item in _mapVertexItems[vertex])
                {
                    if (!mapVertexAndIsMarked[item])
                    {
                        mapVertexAndIsMarked[item] = true;
                        verticesQueue.Enqueue(item);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly Dictionary<TValue, IReadOnlyList<TValue>> _mapVertexItems = new Dictionary<TValue, IReadOnlyList<TValue>>();
    }
}