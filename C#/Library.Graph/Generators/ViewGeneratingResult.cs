using System;
using System.Linq;

using Library.Graph.Views;

namespace Library.Graph.Generators
{
    public sealed class ViewGeneratingResult<TView, TViewItem, TValue>
        where TView : IGraphView<TViewItem, TValue>
        where TViewItem : IGraphViewItem<TValue>
    {
        public TView View { get; }

        public static ViewGeneratingResult<TView, TViewItem, TValue> Create(TView view)
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (!view.Items.Any())
            {
                throw new ArgumentException("The view items collection is empty.", nameof(view.Items));
            }
            return new ViewGeneratingResult<TView, TViewItem, TValue>(view);
        }

        private ViewGeneratingResult(TView view)
        {
            View = view;
        }
    }
}
