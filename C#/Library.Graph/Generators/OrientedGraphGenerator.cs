using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    public class OrientedGraphGenerator<TValue> : GraphGenerator<AdjacensiesBasedGraph<TValue>, AdjacensyGraphItem<TValue>, TValue, OrientedGraphGeneratorOptions<TValue>>
        where TValue : notnull
    {
        public OrientedGraphGenerator(OrientedGraphGeneratorOptions<TValue> orientedView)
            : base(orientedView)
        { }

        protected override GraphGeneratingResult<AdjacensiesBasedGraph<TValue>, AdjacensyGraphItem<TValue>, TValue> BuildCore()
        {
            Debug.Assert(Enum.IsDefined(Options.Connectivity), "Fail never happens.");

            var items = Options.Connectivity switch
            {
                ConnectivityType.NotConnected => CreateNotConnected(),
                ConnectivityType.WeaklyOrJustConnected => CreateWeaklyOrJustConnected(),
                ConnectivityType.StronglyConnected => CreateStronglyConnected(),
                _ => throw new InvalidOperationException($"Received unknown connectivity type '{Options.Connectivity}'.")
            };

            return new GraphGeneratingResult<AdjacensiesBasedGraph<TValue>, AdjacensyGraphItem<TValue>, TValue>(
                new AdjacensiesBasedGraph<TValue>(items.ToList(), MapVertexAndLists.Keys, true, Options.Connectivity));
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
                if (kv.Value.Count <= Options.VerticesCount - skippedVerticesCount - 1)
                {
                    while (kv.Value.Count > kv.Value.Items.Count)
                    {
                        var vertex = GetRandomVertexFrom(vertices);
                        if (!IsLoop(vertex, kv.Key)
                            && !IsContainsDuplicate(vertex, kv.Value.Items)
                            && !skippedVertices.Contains(vertex))
                        {
                            _ = kv.Value.Items.Add(vertex);
                        }
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyGraphItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyGraphItem<TValue>> CreateWeaklyOrJustConnected()
        {
            var vertices = MapVertexAndLists.Keys.ToList();

            var last = MapVertexAndLists.Last().Key;
            var isLastReached = false;
            foreach (var kv in MapVertexAndLists.Where(c => !c.Key.Equals(last)))
            {
                while (kv.Value.Count > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items))
                    {
                        if (vertex.Equals(last))
                        {
                            isLastReached = true;
                        }
                        _ = kv.Value.Items.Add(vertex);
                    }
                }
            }
            while (!isLastReached)
            {
                var vertex = GetRandomVertexFrom(vertices);
                if (!vertex.Equals(last) && !IsContainsDuplicate(vertex, MapVertexAndLists[vertex].Items))
                {
                    _ = MapVertexAndLists[vertex].Items.Add(last);
                    isLastReached = true;
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyGraphItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyGraphItem<TValue>> CreateStronglyConnected()
        {
            var vertices = MapVertexAndLists.Keys.ToList();

            //Explicit make strong connection.
            _ = MapVertexAndLists.Aggregate((f, s) =>
              {
                  _ = f.Value.Items.Add(s.Key);
                  return s;
              });
            _ = MapVertexAndLists.Last().Value.Items.Add(MapVertexAndLists.First().Key);

            foreach (var kv in MapVertexAndLists)
            {
                while (kv.Value.Count > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items))
                    {
                        _ = kv.Value.Items.Add(vertex);
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyGraphItem<TValue>(kv.Key, kv.Value.Items));
        }
    }
}
