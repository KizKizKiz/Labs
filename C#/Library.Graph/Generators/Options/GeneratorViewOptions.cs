using Library.Graph.Views;
using System;

namespace Library.Graph.Generators.Options
{
    public abstract class GeneratorViewOptions<TView, TViewItem, TValue>
        where TView: IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        public int VerticesCount { get; }

        public int MeanConnectivity { get; }

        public Func<TValue> VerticiesFactory { get; }

        public GeneratorViewOptions(
            int verticesCount, 
            int meanConnectivity, 
            Func<TValue> factory)
        {
            if (verticesCount <= 0)
            {
                throw new ArgumentException("Vertices count must be greater than zero.", nameof(verticesCount));
            }
            if (meanConnectivity <= 0 || verticesCount < meanConnectivity)
            {
                throw new ArgumentException($"Mean connectivity must be greater than zero and equal or less than {nameof(verticesCount)}.", nameof(meanConnectivity));
            }
            VerticesCount = verticesCount;
            MeanConnectivity = meanConnectivity;
            VerticiesFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
    }
}
