using System.Linq;
using System.Collections.Generic;

using Library.Graph.Types;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    /// <summary>
    /// Представляет генератор неориентированных графов.
    /// </summary>
    /// <typeparam name="TValue">Тип элементов графа.</typeparam>
    public class UnorientedGraphGenerator<TValue> : GraphGenerator<Graph<TValue>, TValue, UnorientedGraphGeneratorOptions<TValue>>
        where TValue : notnull
    {
        /// <summary>
        /// Конструктор генератора.
        /// </summary>
        /// <param name="options">Настройки генерации.</param>
        public UnorientedGraphGenerator(UnorientedGraphGeneratorOptions<TValue> orientedView)
            : base(orientedView)
        { }

        /// <inheritdoc/>
        protected override GraphGeneratingResult<Graph<TValue>, TValue> BuildCore()
        {
            var items = Options.IsConnected ? CreateConnected() : CreateNotConnected();

            return new GraphGeneratingResult<Graph<TValue>, TValue>(
                new Graph<TValue>(
                    items,
                    MapVertexAndLists.Keys,
                    false,
                    Options.IsConnected ? ConnectivityType.WeaklyOrJustConnected : ConnectivityType.NotConnected));
        }

        private IEnumerable<AdjacensyEdgeItem<TValue>> CreateConnected()
        {
            var vertices = MapVertexAndLists.Keys.ToList();

            foreach (var kv in MapVertexAndLists)
            {
                while (kv.Value.Count > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items.Select(c => c.Target)))
                    {
                        _ = kv.Value.Items.Add(new EdgeItem<TValue>(kv.Key, vertex));
                        if (!IsContainsDuplicate(kv.Key, MapVertexAndLists[vertex].Items.Select(c => c.Target)))
                        {
                            _ = MapVertexAndLists[vertex].Items.Add(new EdgeItem<TValue>(vertex, kv.Key));
                        }
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyEdgeItem<TValue>(kv.Key, kv.Value.Items));
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
                while (kv.Value.Count > kv.Value.Items.Count && kv.Value.Count > skippedVerticesCount)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items.Select(c => c.Target))
                        && !skippedVertices.Contains(vertex))
                    {
                        _ = kv.Value.Items.Add(new EdgeItem<TValue>(kv.Key, vertex));
                        if (!IsContainsDuplicate(kv.Key, MapVertexAndLists[vertex].Items.Select(c => c.Target)))
                        {
                            _ = MapVertexAndLists[vertex].Items.Add(new EdgeItem<TValue>(vertex, kv.Key));
                        }
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyEdgeItem<TValue>(kv.Key, kv.Value.Items));
        }
    }
}
