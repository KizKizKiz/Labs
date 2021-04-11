using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using Library.Graph.Types;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет генератор ориентированных графов.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class OrientedGraphGenerator<TValue> : GraphGenerator<Graph<TValue>, TValue, OrientedGraphGeneratorOptions<TValue>>
        where TValue : notnull
    {
        /// <summary>
        /// Конструктор генератора.
        /// </summary>
        /// <param name="options">Настройки генерации.</param>
        public OrientedGraphGenerator(OrientedGraphGeneratorOptions<TValue> options)
            : base(options)
        { }

        /// <inheritdoc/>
        protected override GraphGeneratingResult<Graph<TValue>, TValue> BuildCore()
        {
            Debug.Assert(Enum.IsDefined(Options.Connectivity), "Fail never happens.");

            var items = Options.Connectivity switch
            {
                ConnectivityType.NotConnected => CreateNotConnected(),
                ConnectivityType.WeaklyOrJustConnected => CreateWeaklyOrJustConnected(),
                ConnectivityType.StronglyConnected => CreateStronglyConnected(),
                _ => throw new InvalidOperationException($"Received unknown connectivity type '{Options.Connectivity}'.")
            };

            return new GraphGeneratingResult<Graph<TValue>, TValue>(
                new Graph<TValue>(items.ToList(), MapVertexAndLists.Keys, true, Options.Connectivity));
        }

        private IEnumerable<AdjacensyEdgeItem<TValue>> CreateNotConnected()
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
                            && !IsContainsDuplicate(vertex, kv.Value.Items.Select(c => c.Target))
                            && !skippedVertices.Contains(vertex))
                        {
                            var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                            _ = kv.Value.Items.Add(new EdgeItem<TValue>(kv.Key, vertex, weight));
                        }
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyEdgeItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyEdgeItem<TValue>> CreateWeaklyOrJustConnected()
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
                        && !IsContainsDuplicate(vertex, kv.Value.Items.Select(c => c.Target)))
                    {
                        if (vertex.Equals(last))
                        {
                            isLastReached = true;
                        }
                        var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                        _ = kv.Value.Items.Add(new EdgeItem<TValue>(kv.Key, vertex, weight));
                    }
                }
            }
            while (!isLastReached)
            {
                var vertex = GetRandomVertexFrom(vertices);
                if (!vertex.Equals(last) && !IsContainsDuplicate(vertex, MapVertexAndLists[vertex].Items.Select(c => c.Target)))
                {
                    var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                    _ = MapVertexAndLists[vertex].Items.Add(new EdgeItem<TValue>(vertex, last, weight));
                    isLastReached = true;
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyEdgeItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyEdgeItem<TValue>> CreateStronglyConnected()
        {
            var vertices = MapVertexAndLists.Keys.ToList();

            //Explicit make strong connection.
            _ = MapVertexAndLists.Aggregate((f, s) =>
              {
                  var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                  _ = f.Value.Items.Add(new EdgeItem<TValue>(f.Key, s.Key, weight));
                  return s;
              });

            var weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);

            var lastPair = MapVertexAndLists.Last();
            _ = lastPair.Value.Items.Add(new EdgeItem<TValue>(lastPair.Key, MapVertexAndLists.First().Key, weight));

            foreach (var kv in MapVertexAndLists)
            {
                while (kv.Value.Count > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items.Select(c => c.Target)))
                    {
                        weight = Randomizer.FromRange(Options.Range.minimum, Options.Range.maximum);
                        _ = kv.Value.Items.Add(new EdgeItem<TValue>(kv.Key, vertex, weight));
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyEdgeItem<TValue>(kv.Key, kv.Value.Items));
        }
    }
}
