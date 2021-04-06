using System;
using System.Collections.Generic;

using MathNet.Numerics.Distributions;

using Library.Graph.Views;
using Library.Graph.Generators.Options;
using System.Linq;

namespace Library.Graph.Generators
{
    public abstract class GraphViewGenerator<TView, TViewItem, TValue> : IGraphViewGenerator<TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TValue : notnull
    {
        public GraphViewGenerator(GeneratorViewOptions<TValue> options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ViewGeneratingResult<TValue> Generate()
        {
            Initialize();
            return BuildCore();
        }

        protected GeneratorViewOptions<TValue> Options { get; }

        protected Dictionary<TValue, (int Count, HashSet<TValue> Items)> MapVertexAndLists { get; } = new();

        protected Random Random { get; } = new((int)DateTime.Now.Ticks & 0x0000FFFFF);

        protected abstract ViewGeneratingResult<TValue> BuildCore();

        protected virtual void Initialize()
        {
            while (MapVertexAndLists.Count != Options.VerticesCount)
            {
                var vertex = Options.VerticiesFactory();
                if (!MapVertexAndLists.ContainsKey(vertex))
                {
                    var elements = Poisson.Sample(Random, Options.MeanConnectivity);

                    elements = elements <= 0 ? 1 : (elements >= Options.VerticesCount ? Options.VerticesCount - 1 : elements);

                    MapVertexAndLists.Add(vertex, (Count: elements, Items: new HashSet<TValue>()));
                }
            }
        }

        protected bool IsLoop(TValue vertexFrom, TValue vertexTo)
            => vertexFrom.Equals(vertexTo);

        protected bool IsContainsDuplicate(TValue vertex, IEnumerable<TValue> items)
            => items.Contains(vertex);

        protected TValue GetRandomVertexFrom(IReadOnlyList<TValue> vertices)
            => vertices[Random.Next(vertices.Count)];
    }
}
