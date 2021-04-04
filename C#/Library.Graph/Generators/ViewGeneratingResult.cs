using System;
using System.Linq;

using Library.Graph.Views;

namespace Library.Graph.Generators
{
    public abstract class ViewGeneratingResult<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
        where TValue : notnull
    {
        public TView View { get; }

        public ViewGeneratingResult(TView view)
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (!view.Items.Any())
            {
                throw new ArgumentException("The view items collection is empty.", nameof(view.Items));
            }
            View = view;
        }
    }
}