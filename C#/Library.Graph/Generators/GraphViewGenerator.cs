using System;
using System.Collections.Generic;

using MathNet.Numerics.Distributions;

using Library.Graph.Views;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    public abstract class GraphViewGenerator<TView, TViewItem, TValue> : IGraphViewGenerator<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TValue : notnull
    {
        public GraphViewGenerator(GeneratorViewOptions<TView, TViewItem, TValue> options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ViewGeneratingResult<TView, TViewItem, TValue> Build()
        {
            Initialize();
            return BuildCore();
        }

        protected GeneratorViewOptions<TView, TViewItem, TValue> Options { get; }

        protected Dictionary<TValue, (int Count, HashSet<TValue> Items)> MapVertexAndLists { get; } = new();

        protected Random Random { get; } = new((int)DateTime.Now.Ticks & 0x0000FFFFF);

        protected abstract ViewGeneratingResult<TView, TViewItem, TValue> BuildCore();

        protected virtual void Initialize()
        {
            while (MapVertexAndLists.Count != Options.VerticesCount) 
            {
                var vertex = Options.VerticiesFactory();
                if (MapVertexAndLists.ContainsKey(vertex))
                {
                    var elements = Poisson.Sample(Random, Options.MeanConnectivity);

                    elements = elements <= 0 ? 1 : (elements >= Options.VerticesCount ? Options.VerticesCount : elements);

                    MapVertexAndLists.Add(vertex, (Count: elements, Items: new HashSet<TValue>()));
                }
            }
        }
    }
}
