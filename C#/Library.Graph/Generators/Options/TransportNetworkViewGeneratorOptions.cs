using Library.Graph.Views;
using System;

namespace Library.Graph.Generators.Options
{
    public sealed class TransportNetworkViewGeneratorOptions<TView, TViewItem, TValue> : GeneratorViewOptions<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        public int SourceOutVertices { get; }

        public int TargetInVertices { get; }

        public TransportNetworkViewGeneratorOptions(
            int verticesCountWithOutSourceAndTarget,
            int meanCohesion,
            Func<TValue> factory,
            int sourceOutVertices,
            int targetInVertices)
            : base(verticesCountWithOutSourceAndTarget, meanCohesion, factory)
        {
            if (sourceOutVertices <= 0)
            {
                throw new ArgumentException("The number of output vertices from 'SOURCE' must be greater than zero and equal or less than vertices count .", nameof(sourceOutVertices));
            }
            if (targetInVertices <= 0)
            {
                throw new ArgumentException("The number of input vertices to 'TARGET' must be greater than zero and equal or less than vertices count .", nameof(targetInVertices));
            }
            SourceOutVertices = sourceOutVertices;
            TargetInVertices = targetInVertices;
        }
    }
}
