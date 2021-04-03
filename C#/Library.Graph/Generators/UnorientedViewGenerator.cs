using System;

using Library.Graph.Views;
using Library.Graph.Generators.Options;

namespace Library.Graph.Generators
{
    public sealed class UnorientedViewGenerator<TView, TViewItem, TValue> : GraphViewGenerator<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TValue : notnull
        {
        public UnorientedViewGenerator(UnorientedViewGeneratorOptions<TView, TViewItem, TValue> options)
            : base(options)
        {
        }

        protected override ViewGeneratingResult<TView, TViewItem, TValue> BuildCore()
        {
            throw new NotImplementedException();
        }
    }
}
