using Library.Graph.Views;
using System;

namespace Library.Graph.Generators.Options
{
    public sealed class UnorientedViewGeneratorOptions<TView, TViewItem, TValue> : GeneratorViewOptions<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        public bool IsConnected { get; }

        public UnorientedViewGeneratorOptions(
            int verticesCount,
            int meanCohesion,
            Func<TValue> factory,
            bool isConnected = true)
            :base(verticesCount, meanCohesion, factory)
        {
            IsConnected = isConnected;
        }
    }
}
