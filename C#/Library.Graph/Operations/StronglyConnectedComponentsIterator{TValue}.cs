using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет итератор поиска максимально связных компонент.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class StronglyConnectedComponentsIterator<TValue> : IEnumerable<IEnumerable<TValue>>
        where TValue : notnull, IEqualityComparer<TValue>, IEquatable<TValue>
    {
        private class StronglyConnectedComponentItem
        {
            public int? Id { get; private set; }

            public int LowLinkId { get; private set; }

            public IReadOnlyCollection<TValue> Items => _components;

            public StronglyConnectedComponentItem()
            {
                LowLinkId = -1;
            }

            public StronglyConnectedComponentItem(int id, int llid)
            {
                Id = id;
                LowLinkId = llid;
            }

            public void SetMinLLIdByLLID(StronglyConnectedComponentItem idAndLowLinkId)
                => LowLinkId = Math.Min(LowLinkId, idAndLowLinkId.LowLinkId);

            public void SetMinLLIdByIndex(StronglyConnectedComponentItem idAndLowLinkId)
            {
                if (idAndLowLinkId.Id is null)
                {
                    throw new ArgumentNullException(nameof(idAndLowLinkId));
                }

                LowLinkId = Math.Min(LowLinkId, idAndLowLinkId.Id.Value);
            }

            public void AddElements(TValue value)
                => _components.Enqueue(value);

            public IEnumerator<TValue> GetEnumerator()
                => _components.GetEnumerator();

            private readonly Queue<TValue> _components = new();
        }

        /// <summary>
        /// Конструктор итератора максимально связных компонент.
        /// </summary>
        /// <typeparam name="TValue">Тип элементов графа.</typeparam>
        public StronglyConnectedComponentsIterator(Graph<TValue> graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.IsOriented)
            {
                throw new ArgumentException("The algorithm support adjacensies based 'oriented' graph.", nameof(graph));
            }
            _graph = graph;

            _mapVertexAndItems = graph.Adjacensies.ToDictionary(i => i.Vertex, i => (IReadOnlyList<TValue>)i.Items.Select(c => c.value).ToList());
            _mapVertexAndSCC = _graph.Items
                .ToDictionary(
                    v => v.Key,
                    _ => new StronglyConnectedComponentItem());

            _vertices = new Stack<TValue>();
        }

        /// <summary>
        /// Возвращает итератор поиска максимально связной компоненты.
        /// </summary>
        public IEnumerator<IEnumerable<TValue>> GetEnumerator()
        {
            foreach (var vertex in _mapVertexAndSCC.Keys)
            {
                if (_mapVertexAndSCC[vertex].Id is null)
                {
                    SetupIterator(vertex);
                }
            }

            var maxElementsInComponent = -1;
            foreach (var item in _mapVertexAndSCC
                .Values
                .OrderByDescending(component => component.Items.Count)
                .Select(component => component.Items))
            {
                if (maxElementsInComponent is -1 || maxElementsInComponent == item.Count)
                {
                    maxElementsInComponent = item.Count;
                    yield return item.Reverse();
                }
                else
                {
                    yield break;
                }
            }
        }

        private void SetupIterator(TValue vertex)
        {
            _mapVertexAndSCC[vertex] = new StronglyConnectedComponentItem(_nextIndex, _nextIndex++);

            _vertices.Push(vertex);

            foreach (var successor in _mapVertexAndItems[vertex])
            {
                if (_mapVertexAndSCC[successor].Id is null)
                {
                    SetupIterator(successor);
                    _mapVertexAndSCC[vertex].SetMinLLIdByLLID(_mapVertexAndSCC[successor]);
                }
                else if (_vertices.Contains(successor))
                {
                    _mapVertexAndSCC[vertex].SetMinLLIdByIndex(_mapVertexAndSCC[successor]);
                }
            }
            if (_mapVertexAndSCC[vertex].LowLinkId == _mapVertexAndSCC[vertex].Id)
            {
                TValue poppedVertex;
                do
                {
                    poppedVertex = _vertices.Pop();
                    _mapVertexAndSCC[vertex].AddElements(poppedVertex);

                } while (!poppedVertex.Equals(vertex));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int _nextIndex;
        private readonly Stack<TValue> _vertices;
        private readonly Dictionary<TValue, StronglyConnectedComponentItem> _mapVertexAndSCC;
        private readonly Graph<TValue> _graph;
        private readonly Dictionary<TValue, IReadOnlyList<TValue>> _mapVertexAndItems;
    }
}