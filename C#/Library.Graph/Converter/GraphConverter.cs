using System;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;

namespace Library.Graph.Converter
{
    public sealed class GraphConverter
    {
        public GraphConverter(IRandomizer randomizer)
        {
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public AdjacensiesBasedGraph<TValue> Convert<TValue>(EdgesBasedGraph<TValue> graph)
            where TValue : notnull
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return CreateAdjacensiesView();

            AdjacensiesBasedGraph<TValue> CreateAdjacensiesView()
            {
                var mapVertexAndList = new Dictionary<TValue, List<TValue>>();
                foreach (var item in graph.Items)
                {
                    if (!mapVertexAndList.ContainsKey(item.First))
                    {
                        mapVertexAndList[item.First] = new List<TValue>();
                    }
                    if (item.Second is not null)
                    {
                        mapVertexAndList[item.First].Add(item.Second!);
                    }
                }
                return new AdjacensiesBasedGraph<TValue>(
                    mapVertexAndList.Select(kv => new AdjacensyGraphItem<TValue>(kv.Key, kv.Value)),
                    graph.Vertices,
                    graph.IsOriented,
                    graph.ConnectivityType);
            }
        }

        public EdgesBasedGraph<TValue> Convert<TValue>(AdjacensiesBasedGraph<TValue> graph, bool isWeighted)
            where TValue : notnull
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            return new EdgesBasedGraph<TValue>(
                CreateEdges(),
                graph.Vertices,
                isWeighted,
                graph.IsOriented,
                graph.ConnectivityType);

            IEnumerable<EdgesViewItem<TValue>> CreateEdges()
            {
                var edges = new List<EdgesViewItem<TValue>>();
                foreach (var item in graph.Items)
                {
                    if (!item.Items.Any())
                    {
                        edges.Add(new EdgesViewItem<TValue>(item.Vertex));
                    }
                    else
                    {
                        edges.AddRange(item.Items.Select(i => isWeighted ?
                            new EdgesViewItem<TValue>(item.Vertex, i, _randomizer.FromRange(-100, 100)) :
                            new EdgesViewItem<TValue>(item.Vertex, i)));
                    }
                }
                return edges;
            }
        }

        private readonly IRandomizer _randomizer;
    }
}