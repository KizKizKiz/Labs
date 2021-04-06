using System;

using Library.Graph.Views;
using Library.Graph.Generators.Options;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Generators
{
    public sealed class TransportNetworkViewGenerator<TValue> : GraphViewGenerator<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : notnull
    {
        public TransportNetworkViewGenerator(TransportNetworkViewGeneratorOptions<TValue> options)
            : base(options)
        { }

        protected override ViewGeneratingResult<TValue> BuildCore()
        {
            var options = (TransportNetworkViewGeneratorOptions<TValue>)Options;
            var vertices = MapVertexAndLists.Keys.ToList();

            var source = GetRandomVertexFrom(vertices);
            var target = GetRandomVertexFrom(vertices);
            while (target.Equals(source))
            {
                target = GetRandomVertexFrom(vertices);
            }

            InitSource(options, vertices, source);

            InitBody(options, vertices, source, target);

            return new ViewGeneratingResult<TValue>(
                new AdjacensiesView<TValue>(
                    MapVertexAndLists.Select(
                        kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items)),
                    MapVertexAndLists.Keys));
        }

        private void InitBody(TransportNetworkViewGeneratorOptions<TValue> options, List<TValue> vertices, TValue source, TValue target)
        {
            var vertexReachTargetCount = 0;
            foreach (var kv in MapVertexAndLists)
            {
                if (kv.Key.Equals(target) || kv.Key.Equals(source))
                {
                    continue;
                }

                var verticesCount = kv.Value.Count > options.VerticesCount - 2 ? options.VerticesCount - 2: kv.Value.Count; //verticesCount - 2 (source, target)
                while (verticesCount > kv.Value.Items.Count)
                {
                    var vertex = GetRandomVertexFrom(vertices);
                    if (!IsLoop(vertex, kv.Key)
                        && !IsContainsDuplicate(vertex, kv.Value.Items)
                        && !vertex.Equals(source))
                    {
                        _ = kv.Value.Items.Add(vertex);
                    }
                    if (vertex.Equals(target))
                    {
                        vertexReachTargetCount++;
                    }
                }
            }

            while (vertexReachTargetCount < options.TargetMinInVertices)
            {
                var vertex = GetRandomVertexFrom(vertices);
                if (!vertex.Equals(target) && !MapVertexAndLists[vertex].Items.Contains(target))
                {
                    _ = MapVertexAndLists[vertex].Items.Add(target);
                    vertexReachTargetCount++;
                }
            }
        }

        private void InitSource(TransportNetworkViewGeneratorOptions<TValue> options, List<TValue> vertices, TValue source)
        {
            while (MapVertexAndLists[source].Items.Count != options.SourceOutVertices)
            {
                var vertex = GetRandomVertexFrom(vertices);
                if (!IsLoop(vertex, source)
                    && !IsContainsDuplicate(vertex, MapVertexAndLists[source].Items))
                {
                    _ = MapVertexAndLists[source].Items.Add(vertex);
                }
            }
        }
    }
}
