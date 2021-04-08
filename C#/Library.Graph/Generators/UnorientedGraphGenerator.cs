using System;
using System.Linq;
using System.Collections.Generic;

using Library.Graph.Types;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    public class UnorientedGraphGenerator<TValue> : GraphGenerator<AdjacensiesBasedGraph<TValue>, AdjacensyGraphItem<TValue>, TValue, UnorientedGraphGeneratorOptions<TValue>>
        where TValue : notnull
    {
        public UnorientedGraphGenerator(UnorientedGraphGeneratorOptions<TValue> orientedView)
            : base(orientedView)
        { }

        protected override GraphGeneratingResult<AdjacensiesBasedGraph<TValue>, AdjacensyGraphItem<TValue>, TValue> BuildCore()
        {
            var items = Options.IsConnected ? CreateConnected() : CreateNotConnected();
            
            return new GraphGeneratingResult<AdjacensiesBasedGraph<TValue>, AdjacensyGraphItem<TValue>, TValue>(
                new AdjacensiesBasedGraph<TValue>(
                    items, 
                    MapVertexAndLists.Keys, 
                    false, 
                    Options.IsConnected ? ConnectivityType.WeaklyOrJustConnected : ConnectivityType.NotConnected));
        }

        private IEnumerable<AdjacensyGraphItem<TValue>> CreateConnected()
        {
            var vertices = MapVertexAndLists.Keys.ToList();

            foreach (var kv in MapVertexAndLists)
            {
                while (kv.Value.Count > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items))
                    {
                        _ = kv.Value.Items.Add(vertex);
                        if (!IsContainsDuplicate(kv.Key, MapVertexAndLists[vertex].Items))
                        {
                            _ = MapVertexAndLists[vertex].Items.Add(kv.Key);
                        }
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyGraphItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyGraphItem<TValue>> CreateNotConnected()
        {
            var skippedVertices = new HashSet<TValue>();
            var skippedVerticesCount = Randomizer.FromRange(1, Options.VerticesCount);
            var vertices = MapVertexAndLists.Keys.ToList();
            while (skippedVertices.Count != skippedVerticesCount)
            {
                _ = skippedVertices.Add(vertices[Randomizer.FromRange(vertices.Count)]);
            }

            foreach (var kv in MapVertexAndLists)
            {
                if (skippedVertices.Contains(kv.Key))
                {
                    continue;
                }
                while (kv.Value.Count > kv.Value.Items.Count && kv.Value.Count > skippedVerticesCount)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key) 
                        && !IsContainsDuplicate(vertex, kv.Value.Items) 
                        && !skippedVertices.Contains(vertex))
                    {
                        _ = kv.Value.Items.Add(vertex);
                        if (!IsContainsDuplicate(kv.Key, MapVertexAndLists[vertex].Items))
                        {
                            _ = MapVertexAndLists[vertex].Items.Add(kv.Key);
                        }
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyGraphItem<TValue>(kv.Key, kv.Value.Items));
        }
    }
}
