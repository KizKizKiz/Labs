using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Types.Adjacensies;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет реализацию обхода в ширину неориентированного графа на списках смежности.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class BFSIterator<TValue> : IEnumerable<TValue>
         where TValue : IStringConvertible<TValue>, new()
    {
        public BFSIterator(UnorientedAdjacensiesGraph<TValue> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (graph.View is null)
            {
                throw new ArgumentException("The view is null.");
            }
            if (!graph.View.Items.Any())
            {
                throw new ArgumentException("The graph is empty.");
            }
            _mapVertexItems = graph
                .View
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