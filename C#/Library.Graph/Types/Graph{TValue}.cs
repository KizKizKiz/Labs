using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Library.Graph.Generators.Options;

namespace Library.Graph.Types
{
    /// <summary>
    /// Представляет граф с элементами типа <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class Graph<TValue>
        where TValue : notnull
    {
        private static class GraphDeterminantHelper
        {
            public static ConnectivityType DeterminateConnectivity(Graph<TValue> graph)
            {
                if (graph is null)
                {
                    throw new ArgumentNullException(nameof(graph));
                }

                return graph.IsOriented ?
                    DeterminateConnectivityForOrientedCore(graph)
                    : DeterminateConnectivityForNotOrientedCore(graph);
            }

            private static ConnectivityType DeterminateConnectivityForNotOrientedCore(Graph<TValue> graph)
            {
                var mapVertexAndItems = graph.Items.ToDictionary(i => i.Key, i => i.Value.Items.Select(c => c.Target));
                var mapVertexAndIsMarked = graph.Items.ToDictionary(v => v.Key, _ => false);

                var verticesQueue = new Queue<TValue>();

                verticesQueue.Enqueue(mapVertexAndIsMarked.Keys.First());

                while (verticesQueue.Any())
                {
                    var vertex = verticesQueue.Dequeue();

                    foreach (var item in mapVertexAndItems[vertex])
                    {
                        if (!mapVertexAndIsMarked[item])
                        {
                            mapVertexAndIsMarked[item] = true;
                            verticesQueue.Enqueue(item);
                        }
                    }
                }

                return mapVertexAndIsMarked.Values.All(c => c) ?
                    ConnectivityType.WeaklyOrJustConnected
                    : ConnectivityType.NotConnected;
            }

            private static ConnectivityType DeterminateConnectivityForOrientedCore(Graph<TValue> graph)
            {
                var mapVertexAndItems = graph.Items
                    .ToDictionary(
                    i => i.Key,
                    i => (IsAllReached: false, i.Value.Items));

                var vertices = mapVertexAndItems.Keys.ToHashSet();

                foreach (var adjacensy in graph.Items)
                {
                    var mapVertexAndIsMarked = graph.Items
                        .ToDictionary(
                        v => v.Key,
                        _ => false);

                    mapVertexAndIsMarked[adjacensy.Key] = true;

                    var verticesQueue = new Queue<TValue>();
                    verticesQueue.Enqueue(adjacensy.Key);

                    while (verticesQueue.Any())
                    {
                        var vertex = verticesQueue.Dequeue();
                        foreach (var value in mapVertexAndItems[vertex].Items.Select(c => c.Target))
                        {
                            if (!mapVertexAndIsMarked[value])
                            {
                                _ = vertices.Remove(vertex);
                                _ = vertices.Remove(value);
                                mapVertexAndIsMarked[value] = true;
                                verticesQueue.Enqueue(value);
                            }
                        }
                    }
                    if (mapVertexAndIsMarked.Values.All((c) => c))
                    {
                        mapVertexAndItems[adjacensy.Key] = (true, mapVertexAndItems[adjacensy.Key].Items);
                    }
                }
                if (vertices.Count != 0)
                {
                    return ConnectivityType.NotConnected;
                }
                return mapVertexAndItems.Values.Any(c => c.IsAllReached == false)
                    ? ConnectivityType.WeaklyOrJustConnected
                    : ConnectivityType.StronglyConnected;
            }
        }

        /// <summary>
        /// Словарь из ключа в виде вершины и значения в виде списка смежностей, который состоит из ребер.
        /// </summary>
        public IReadOnlyDictionary<TValue, AdjacensyEdgeItem<TValue>> Items { get; }

        /// <summary>
        /// Итератор элементов смежностей.
        /// </summary>
        public IEnumerable<AdjacensyItem<TValue>> Adjacensies => SetupAdjacensiesIterator();

        /// <summary>
        /// Итератор ребер.
        /// </summary>
        public IEnumerable<EdgeItem<TValue>> Edges => SetupEdgesIterator();

        /// <summary>
        /// Множество вершин.
        /// </summary>
        public IReadOnlyList<TValue> Vertices { get; }

        /// <summary>
        /// Флаг, является ли граф взвешенным.
        /// </summary>
        public bool IsWeighted { get; private set; }

        /// <summary>
        /// Флаг, является ли граф ориентированным.
        /// </summary>
        public bool IsOriented { get; }

        /// <summary>
        /// Тип связности графа.
        /// </summary>
        public ConnectivityType ConnectivityType { get; }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="items">Список смежностей, который состоит из ребер.</param>
        /// <param name="vertices">Множество вершин.</param>
        /// <param name="isOriented">Флаг, является ли граф ориентированным.</param>
        public Graph(
            IEnumerable<AdjacensyEdgeItem<TValue>> items,
            IEnumerable<TValue> vertices,
            bool isOriented)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (vertices is null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }
            if (!items.Any())
            {
                throw new ArgumentException("The items collection is empty.", nameof(items));
            }
            if (!vertices.Any())
            {
                throw new ArgumentException("The vertices collection is empty.", nameof(vertices));
            }

            Vertices = vertices.ToList();
            Items = items.ToDictionary(c => c.Vertex);
            IsOriented = isOriented;

            VerifyDirection();
            SetIsWeighted();
        }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="items">Список смежностей, который состоит из ребер.</param>
        /// <param name="vertices">Множество вершин.</param>
        /// <param name="isOriented">Флаг, является ли граф ориентированным.</param>
        /// <param name="connectivityType">Тип связности графа.</param>
        public Graph(
            IEnumerable<AdjacensyEdgeItem<TValue>> items,
            IEnumerable<TValue> vertices,
            bool isOriented,
            ConnectivityType connectivityType)
            : this(items, vertices, isOriented)
        {
            if (!Enum.IsDefined(connectivityType))
            {
                throw new InvalidEnumArgumentException(nameof(connectivityType), (int)connectivityType, typeof(ConnectivityType));
            }
            if (!isOriented && connectivityType == ConnectivityType.StronglyConnected)
            {
                throw new ArgumentException($"The undirected graph can be only '{ConnectivityType.NotConnected}' or {ConnectivityType.WeaklyOrJustConnected}.");
            }
            ConnectivityType = connectivityType;

            VerifyConnectivityType();
        }

        /// <summary>
        /// Конструктор графа.
        /// </summary>
        /// <param name="items">Список смежностей, который состоит из ребер.</param>
        /// <param name="vertices">Множество вершин.</param>
        /// <param name="isOriented">Флаг, является ли граф ориентированным.</param>
        /// <param name="connectivityType">Тип связности графа.</param>
        /// <param name="needToValidateConnectivityType">Флаг, нужно ли проверять тип связности графа (по умолчанию - не проверять).</param>
        public Graph(
            IEnumerable<AdjacensyEdgeItem<TValue>> items,
            IEnumerable<TValue> vertices,
            bool isOriented,
            ConnectivityType connectivityType,
            bool needToValidateConnectivityType = false)
            : this(items, vertices, isOriented)
        {
            if (!Enum.IsDefined(connectivityType))
            {
                throw new InvalidEnumArgumentException(nameof(connectivityType), (int)connectivityType, typeof(ConnectivityType));
            }
            if (!isOriented && connectivityType == ConnectivityType.StronglyConnected)
            {
                throw new ArgumentException($"The undirected graph can be only '{ConnectivityType.NotConnected}' or {ConnectivityType.WeaklyOrJustConnected}.");
            }

            ConnectivityType = connectivityType;
            if (needToValidateConnectivityType)
            {
                VerifyConnectivityType();
            }
        }

        /// <summary>
        /// Возвращает список ребер для вершины <paramref name="vertex"/>.
        /// </summary>
        /// <param name="vertex">Вершина графа.</param>
        /// <exception cref="KeyNotFoundException">
        /// Вызывается в случае отсутствия вершины в графе.
        /// </exception>
        public IEnumerable<EdgeItem<TValue>> GetEdgesByVertex(TValue vertex)
        {
            if (vertex is null)
            {
                throw new ArgumentNullException(nameof(vertex));
            }
            return Items.ContainsKey(vertex) ?
                Items[vertex].Items :
                throw new KeyNotFoundException("Vertex is not presented in the graph.");
        }

        private void SetIsWeighted()
        {
            var isAllWeighted = Items.Values.SelectMany(c => c.Items).All(c => c.Weight is not null);

            if (!isAllWeighted && Items.Values.SelectMany(c => c.Items).Any(c => c.Weight is not null))
            {
                throw new ArgumentException("Graph contains a weighted edge and non weighted. Assign weight to all edges or remove weight from it.");
            }
            IsWeighted = isAllWeighted;
        }

        private void VerifyDirection()
        {
            foreach (var adjacensy in Items)
            {
                foreach (var edge in adjacensy.Value.Items)
                {
                    if (!Items[edge.Target].Items.Contains(ReverseEdge(edge)) && !IsOriented)
                    {
                        throw new ArgumentException($"Graph is not an undirected.", nameof(IsOriented));
                    }
                }
            }
        }

        private void VerifyConnectivityType()
        {
            var connectivity = GraphDeterminantHelper.DeterminateConnectivity(this);
            if (connectivity != ConnectivityType)
            {
                throw new ArgumentException($"Connectivity type mismatch.\nReceived '{ConnectivityType}', but detected '{connectivity}'.", nameof(ConnectivityType));
            }
        }

        private static EdgeItem<TValue> ReverseEdge(EdgeItem<TValue> edge)
            => new(edge.Target, edge.Source, edge.Weight ?? 0);


        private IEnumerable<EdgeItem<TValue>> SetupEdgesIterator()
            => Items.Values.Select(c => c.Items).SelectMany(c => c);

        private IEnumerable<AdjacensyItem<TValue>> SetupAdjacensiesIterator()
        {
            var mapVertexAndList = new Dictionary<TValue, List<(double weight, TValue value)>>();
            foreach (var item in Items)
            {
                if (!mapVertexAndList.ContainsKey(item.Value.Vertex))
                {
                    mapVertexAndList[item.Value.Vertex] = new List<(double weight, TValue value)>();
                }
                mapVertexAndList[item.Value.Vertex].AddRange(item.Value.Items.Select(c => (weight: c.Weight ?? 0, value: c.Target)));
            }
            return mapVertexAndList.Select(c => new AdjacensyItem<TValue>(c.Key, c.Value));
        }
    }
}