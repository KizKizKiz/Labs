using System;
using System.Diagnostics;

using Library.Graph.Views;
using Library.Graph.Generators.Options;
using System.Collections.Generic;
using System.Linq;

namespace Library.Graph.Generators
{
    public class OrientedViewGenerator<TValue> : GraphViewGenerator<AdjacensiesView<TValue>, AdjacensyViewItem<TValue>, TValue>
        where TValue : notnull
    {
        public OrientedViewGenerator(OrientedViewGeneratorOptions<TValue> orientedView)
            : base(orientedView)
        { }

        protected override ViewGeneratingResult<TValue> BuildCore()
        {
            var options = (OrientedViewGeneratorOptions<TValue>) Options;

            Debug.Assert(Enum.IsDefined(options.Connectivity), "Fail never happens.");

            var items = options.Connectivity switch
            {
                ConnectivityType.NotConnected => CreateNotConnected(),
                ConnectivityType.WeaklyConnected => CreateWeaklyConnected(),
                ConnectivityType.StronglyConnected => CreateStronglyConnected(),
                _ => throw new InvalidOperationException($"Received unknown connectivity type '{options.Connectivity}'.")
            };

            return new ViewGeneratingResult<TValue>(
                new AdjacensiesView<TValue>(items.ToList(), MapVertexAndLists.Keys));
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

            return MapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyViewItem<TValue>> CreateWeaklyConnected()
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
                    }
                }
            }

            return MapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items));
        }

        private IEnumerable<AdjacensyViewItem<TValue>> CreateStronglyConnected()
        {
            var vertices = MapVertexAndLists.Keys.ToList();

            //Explicit make strong connection.
            MapVertexAndLists.Aggregate((f, s) =>
            {
                f.Value.Items.Add(s.Key);
                return s;
            });
            MapVertexAndLists.Last().Value.Items.Add(MapVertexAndLists.First().Key);

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

            return MapVertexAndLists.Select(kv => new AdjacensyViewItem<TValue>(kv.Key, kv.Value.Items));
        }
    }
}
