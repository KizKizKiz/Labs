using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Converter;
using Library.Graph.Generators.Options;
using Library.Graph.Types;

namespace Library.Graph.Helpers
{
    public sealed class GraphDeterminantHelper<TValue>
        where TValue : notnull
    {
        public GraphDeterminantHelper(GraphConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public ConnectivityType DeterminateConnectivity(EdgesBasedGraph<TValue> graph)
        {
            ValidateGraph(graph);

            return graph.IsOriented ?
                DeterminateConnectivityForOrientedCore(_converter.Convert(graph))
                : DeterminateConnectivityForNotOrientedCore(_converter.Convert(graph));
        }

        public ConnectivityType DeterminateConnectivity(AdjacensiesBasedGraph<TValue> graph)
        {
            ValidateGraph(graph);

            return graph.IsOriented ?
                DeterminateConnectivityForOrientedCore(graph)
                : DeterminateConnectivityForNotOrientedCore(graph);
        }

        public bool DeterminateIsDirected(EdgesBasedGraph<TValue> graph)
        {
            ValidateGraph(graph);

            foreach (var item in graph.Items)
            {
                var otherEdge = item.Second is not null ?
                    new EdgesViewItem<TValue>(item.Second, item.First, item.Weight.HasValue ? item.Weight!.Value : 0)
                    : new EdgesViewItem<TValue>(item.First);

                if (!graph.Items.Contains(otherEdge))
                {
                    return true;
                }
            }
            return false;
        }

        public bool DeterminateIsDirected(AdjacensiesBasedGraph<TValue> graph)
        {
            ValidateGraph(graph);

            var mapVertexAndLists = graph.Items
                .ToDictionary(
                c => c.Vertex,
                c => c.Items);

            foreach (var pair in graph.Items)
            {
                foreach (var item in pair.Items)
                {
                    if (!mapVertexAndLists[item].Contains(pair.Vertex))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private ConnectivityType DeterminateConnectivityForNotOrientedCore(AdjacensiesBasedGraph<TValue> graph)
        {
            var mapVertexAndItems = graph.Items.ToDictionary(i => i.Vertex, i => i.Items);
            var mapVertexAndIsMarked = graph.Items.ToDictionary(v => v.Vertex, _ => false);

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

            return mapVertexAndIsMarked.Values.All(c => c == true) ?
                ConnectivityType.WeaklyOrJustConnected
                : ConnectivityType.NotConnected;
        }

        private ConnectivityType DeterminateConnectivityForOrientedCore(AdjacensiesBasedGraph<TValue> graph)
        {
            var mapVertexAndItems = graph.Items
                .ToDictionary(
                i => i.Vertex,
                i => (IsAllReached : false, Items : i.Items));

            var vertices = mapVertexAndItems.Keys.ToHashSet();

            foreach (var adjacensy in graph.Items)
            {
                var mapVertexAndIsMarked = graph.Items
                    .ToDictionary(
                    v => v.Vertex,
                    _ => false);

                mapVertexAndIsMarked[adjacensy.Vertex] = true;

                var verticesQueue = new Queue<TValue>();
                verticesQueue.Enqueue(adjacensy.Vertex);

                while (verticesQueue.Any())
                {
                    var vertex = verticesQueue.Dequeue();
                    foreach (var item in mapVertexAndItems[vertex].Items)
                    {
                        if (!mapVertexAndIsMarked[item])
                        {
                            _ = vertices.Remove(vertex);
                            _ = vertices.Remove(item);
                            mapVertexAndIsMarked[item] = true;
                            verticesQueue.Enqueue(item);
                        }
                    }
                }
                if (mapVertexAndIsMarked.Values.All((c) => c == true))
                {
                    mapVertexAndItems[adjacensy.Vertex] = (true, mapVertexAndItems[adjacensy.Vertex].Items);
                }
            }
            if (vertices.Count != 0)
            {
                return ConnectivityType.NotConnected;
            }
            if (mapVertexAndItems.Values.Any(c => c.IsAllReached == false))
            {
                return ConnectivityType.WeaklyOrJustConnected;
            }
            return ConnectivityType.StronglyConnected;
        }

        private static void ValidateGraph<TViewItem>(Graph<TViewItem, TValue> graph)
            where TViewItem : IGraphViewItem<TValue>
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.Items.Any())
            {
                throw new ArgumentException("The graph items collection is empty.", nameof(graph.Items));
            }
        }

        private readonly GraphConverter _converter;
    }
}