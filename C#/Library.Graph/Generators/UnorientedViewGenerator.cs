using System;
using System.Linq;
using System.Collections.Generic;

using Library.Graph.Views;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    public class UnorientedViewGenerator<TValue> : GraphViewGenerator<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : notnull
    {
        public UnorientedViewGenerator(UnorientedViewGeneratorOptions<TValue> orientedView)
            : base(orientedView)
        { }

        protected override ViewGeneratingResult<TValue> BuildCore()
        {
            var options = (UnorientedViewGeneratorOptions<TValue>)Options;

            var items = options.IsConnected ? CreateConnected() : CreateNotConnected();
            
            return new ViewGeneratingResult<TValue>(new AdjacensiesView<TValue>(items, MapVertexAndLists.Keys));
        }

        private IEnumerable<AdjacensyViewItem<TValue>> CreateConnected()
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

            return MapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyViewItem<TValue>> CreateNotConnected()
        {
            var skippedVertices = new HashSet<TValue>();
            var skippedVerticesCount = Random.Next(1, Options.VerticesCount);
            var vertices = MapVertexAndLists.Keys.ToList();
            while (skippedVertices.Count != skippedVerticesCount)
            {
                _ = skippedVertices.Add(vertices[Random.Next(vertices.Count)]);
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

            return MapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items));
        }
    }
}
