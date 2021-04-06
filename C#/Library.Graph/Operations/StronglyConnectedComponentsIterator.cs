using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.ConvertibleTypes;
using Library.Graph.Types.Adjacensies;

namespace Library.Graph.Operations
{
    /// <summary>
    /// Представляет итератор поиска максимально связных компонент.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class StronglyConnectedComponentsIterator<TValue> : IEnumerable<IEnumerable<TValue>>
        where TValue : IStringConvertible<TValue>, new()
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
            {
                LowLinkId = Math.Min(LowLinkId, idAndLowLinkId.LowLinkId);
            }

            public void SetMinLLIdByIndex(StronglyConnectedComponentItem idAndLowLinkId)
            {
                if (idAndLowLinkId.Id is null)
                {
                    throw new ArgumentNullException(nameof(idAndLowLinkId.Id));
                }

                LowLinkId = Math.Min(LowLinkId, idAndLowLinkId.Id.Value);
            }

            public void AddElements(TValue value)
                => _components.Enqueue(value);

            public IEnumerator<TValue> GetEnumerator()
                => _components.GetEnumerator();

            private readonly Queue<TValue> _components = new Queue<TValue>();
        }

        public StronglyConnectedComponentsIterator(OrientedAdjacensiesGraph<TValue> graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));

            _mapVertexAndItem = _graph.View.Items
                .ToDictionary(
                    v => v.Vertex,
                    _ => new StronglyConnectedComponentItem());

            _vertices = new Stack<TValue>();
        }

        /// <summary>
        /// Возвращает итератор поиска максимально связной компоненты.
        /// </summary>
        public IEnumerator<IEnumerable<TValue>> GetEnumerator()
        {
            foreach (var vertex in _mapVertexAndItem.Keys)
            {
                if (_mapVertexAndItem[vertex].Id is null)
                {
                    SetupIterator(vertex);
                }
            }

            var maxElementsInComponent = -1;
            foreach (var item in _mapVertexAndItem
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
            _mapVertexAndItem[vertex] = new StronglyConnectedComponentItem(_nextIndex, _nextIndex++);

            _vertices.Push(vertex);

            foreach (var successor in _graph.View.GetValuesByVertex(vertex))
            {
                if (_mapVertexAndItem[successor].Id is null)
                {
                    SetupIterator(successor);
                    _mapVertexAndItem[vertex].SetMinLLIdByLLID(_mapVertexAndItem[successor]);
                }
                else if (_vertices.Contains(successor))
                {
                    _mapVertexAndItem[vertex].SetMinLLIdByIndex(_mapVertexAndItem[successor]);
                }
            }
            if (_mapVertexAndItem[vertex].LowLinkId == _mapVertexAndItem[vertex].Id)
            {
                TValue poppedVertex;
                do
                {
                    poppedVertex = _vertices.Pop();
                    _mapVertexAndItem[vertex].AddElements(poppedVertex);

                } while (!poppedVertex.Equals(vertex));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int _nextIndex;
        private readonly Stack<TValue> _vertices;
        private readonly Dictionary<TValue, StronglyConnectedComponentItem> _mapVertexAndItem;
        private readonly OrientedAdjacensiesGraph<TValue> _graph;
    }
}